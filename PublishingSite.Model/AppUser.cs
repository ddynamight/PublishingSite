using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace PublishingSite.Model
{
     public class AppUser : IdentityUser
     {
          public AppUser()
          {
               this.Papers = new HashSet<Paper>();
          }

          public override string Id { get; set; }
          public string Title { get; set; }
          public string Firstname { get; set; }
          public string Middlename { get; set; }
          public string Lastname { get; set; }
          public string Sex { get; set; }
          public string University { get; set; }
          public string Country { get; set; }
          public string PostCode { get; set; }

          public IEnumerable<Paper> Papers { get; set; }
     }
}
