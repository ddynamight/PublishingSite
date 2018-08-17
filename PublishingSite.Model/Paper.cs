using System;

namespace PublishingSite.Model
{
     public class Paper
    {
          public Paper()
          {
               this.Date = DateTime.Now;
          }

          public int Id { get; set; }
          public string Title  { get; set; }
          public string Abstract { get; set; }
          public string Keywords { get; set; }
          public bool IsPrevSubmitted { get; set; }
          public string PrevSubmission { get; set; }
          public string FundingBody { get; set; }
          public string PaperUrl { get; set; }
          public DateTime Date { get; set; }
          public string Tagname { get; set; }

          public string AppUserId { get; set; }
          public virtual AppUser AppUser { get; set; }
     }
}
