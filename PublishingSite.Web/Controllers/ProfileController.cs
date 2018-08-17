using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PublishingSite.Web.Models.ProfileViewModels;
using PublishingSite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PublishingSite.Web.Controllers
{
     [Authorize()]
     public class ProfileController : Controller
     {
          AppDbContext db = new AppDbContext();

          public async Task<IActionResult> Index()
          {
               return View(await db.AppUsers.SingleAsync(e => e.UserName == User.Identity.Name));
          }

          public async Task<IActionResult> Create()
          {
               #region DDL Stuff Here

               
               string[] countryArray = { "Select Country", "Cameroon", "England", "France", "Ghana", "Nigeria", "South Africa" };
               string[] sexArray = { "Select Sex", "Male", "Female" };
               string[] titleArray = { "Select Title", "Engr", "Dr", "Miss", "Mr", "Mrs", "Pharm", "Prof" };

               ViewData["ddlCountry"] = new SelectList(countryArray);
               ViewData["ddlTitle"] = new SelectList(titleArray);
               ViewData["ddlSex"] = new SelectList(sexArray);

               #endregion

               return View();
          }
          
          [HttpPost]
          public async Task<IActionResult> Create(AddDetailsViewModel model)
          {
               var user = await db.AppUsers.SingleAsync(e => e.UserName == User.Identity.Name);

               user.Title = model.Title;
               user.Firstname = model.Firstname;
               user.Middlename = model.Middlename;
               user.Lastname = model.Lastname;
               user.Sex = model.Sex;
               user.University = model.University;
               user.Country = model.Country;
               user.PostCode = model.PostCode;
               user.PhoneNumber = model.PhoneNumber;

               if (ModelState.IsValid)
               {
                    db.AppUsers.Attach(user);
                    db.Entry(user).State = EntityState.Modified;
                    await db.SaveChangesAsync();
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