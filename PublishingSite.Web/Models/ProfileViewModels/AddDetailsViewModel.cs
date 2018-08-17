using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingSite.Web.Models.ProfileViewModels
{
     public class AddDetailsViewModel
     {
          [Required]
          [Display(Name = "Title")]
          public string Title { get; set; }

          [Required]
          [Display(Name = "Firstname")]
          public string Firstname { get; set; }

          [Required]
          [Display(Name = "Middlename")]
          public string Middlename { get; set; }

          [Required]
          [Display(Name = "Lastname")]
          public string Lastname { get; set; }

          [Required]
          [Display(Name = "Sex")]
          public string Sex { get; set; }

          [Required]
          [Display(Name = "University")]
          public string University { get; set; }

          [Required]
          [Display(Name = "Country")]
          public string Country { get; set; }

          [Required]
          [Display(Name = "PostCode")]
          public string PostCode { get; set; }

          [Required]
          [Phone]
          [Display(Name = "Phone number")]
          public string PhoneNumber { get; set; }

     }
}
