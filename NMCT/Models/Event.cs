using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NMCT.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Display(Name = "Trail")]
        public int? TrailID { get; set; }

        [Required]
        [Display(Name = "Event Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Event Name must be shorter than 100 characters.")]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required]
        [StringLength(512, ErrorMessage = "Description must be at least {2} and no more than {1} characters.", MinimumLength = 100)]
        [Display(Name = "Description")]
        public string EventDescription { get; set; }

        [Display(Name = "Contact by Phone")]
        public string ContactPhone { get; set; }

        [Display(Name = "Contact by Email")]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Display(Name = "Website")]
        public string ContactUrl { get; set; }

        public string Status { get; set; }

        public List<EventPictures> EventPictures { get; set; }

    }

    public class EventPictures
    {
        [Key]
        public int ID { get; set; }
        public int EventID { get; set; }
        public string PictureURL { get; set; }

    }
}