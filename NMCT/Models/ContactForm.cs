using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NMCT.Models
{
    public enum FormCategory
    {
        Suggestion = 1,
        Feedback = 2,
        Technical = 3,
        Other = 4
    }
    public enum FormStatus
    {
        Submitted = 1,
        Assigned = 2,
        Investigating = 3,
        Resolved = 4
    }
    public class ContactForm
    {
        [Key]
        public int FormID { get; set; }

        [Required]
        [Display(Name = "Category")]
        public FormCategory FormCategory { get; set; }

        [Display(Name = "Status")]
        public FormStatus FormStatus { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Assigned User")]
        public string AssignedUserName { get; set; }

        [Display(Name = "Date Submitted")]
        public DateTime SubmissionDate { get; set; }

        [Display(Name = "Date Resolved")]
        public DateTime? ResolvedDate { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title may only be 100 characters long.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(512, ErrorMessage = "Feedback may only be 512 characters long. If we need additional information, we will contact you.")]
        [Display(Name = "Feedback")]
        public string Content { get; set; }

    }
}