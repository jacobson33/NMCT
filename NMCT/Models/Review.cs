using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NMCT.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public int TrailID { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}