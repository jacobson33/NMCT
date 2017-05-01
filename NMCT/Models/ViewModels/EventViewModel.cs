using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NMCT.Models.ViewModels
{
    public class EventViewModel
    {
        public int EventID { get; set; }
        public string TrailName { get; set; }
    
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactUrl { get; set; }
        public string EventPicture { get; set; }
    }
}