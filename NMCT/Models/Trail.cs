using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NMCT.Models
{
    public class Trail
    {
        [Key]
        public int TrailID { get; set; }
        [Required]
        [Display(Name="Trail Name")]
        public string Name { get; set; }
        public string County { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
    }
}