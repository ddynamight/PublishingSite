using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PublishingSite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace PublishingSite.Web.Controllers
{
     [Authorize]
     public class PapersController : Controller
     {
          AppDbContext db = new AppDbContext();

          private IHostingEnvironment _environment;
          public PapersController(IHostingEnvironment environment)
          {
               _environment = environment;
          }

          public async Task<IActionResult> Index()
          {
               return View(await db.Papers.Where(e => e.AppUser.UserName == User.Identity.Name).Include(e => e.AppUser).ToListAsync());
          }


          public async Task<IActionResult> Create(IFormCollection formCollection, IFormFile file)
          {
               var user = await db.AppUsers.SingleAsync(e => e.UserName == User.Identity.Name);
               Paper paper = new Paper();

               paper.Title = formCollection["Title"];
               paper.Keywords = formCollection["Keywords"];
               paper.Date = DateTime.Now;
               paper.FundingBody = formCollection["FundingBody"];
               paper.PrevSubmission = formCollection["PrevSubmission"];
               paper.Abstract = formCollection["Abstract"];
               paper.AppUser = user;
               paper.Tagname = paper.Title.Replace(" ", "-") + "-" + Guid.NewGuid().ToString().Substring(0, 8);

               if (paper.PrevSubmission != null)
               {
                    paper.IsPrevSubmitted = true;
               }
               else
               {
                    paper.IsPrevSubmitted = false;
               }

               string fileGuid = Guid.NewGuid().ToString().ToLower().Substring(0, 10);
               var directory = Path.Combine(_environment.WebRootPath, "PaperFiles");

               if (file.Length > 0)
               {
                    if (!Directory.Exists(directory))
                    {
                         Directory.CreateDirectory(directory);
                    }


                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.GetFullPath(directory);

                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.ReadWrite))
                    {
                         await file.CopyToAsync(fileStream);
                         paper.PaperUrl = fileName;
                    }


                    if (ModelState.IsValid)
                    {
                         db.Papers.Add(paper);
                         db.Entry(paper).State = EntityState.Added;
                         await db.SaveChangesAsync();
                    }
               }

               return RedirectToAction("Index");
          }

          protected override void Dispose(bool disposing)
          {
               db.Dispose();
               base.Dispose(disposing);
          }
     }
}