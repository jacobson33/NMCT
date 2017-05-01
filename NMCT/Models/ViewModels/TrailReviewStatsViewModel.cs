using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMCT.Models.ViewModels
{
    [NotMapped]
    public class TrailReviewStatsViewModel
    {
        public int TrailID { get; set; }
        public double Rating { get; set; }
        public int Rating1 { get; set; }
        public int Rating2 { get; set; }
        public int Rating3 { get; set; }
        public int Rating4 { get; set; }
        public int Rating5 { get; set; }

        public TrailReviewStatsViewModel()
        {

        }

        public TrailReviewStatsViewModel(int? id)
        {
            this.TrailID = id ?? default(int);
        }
    }
}