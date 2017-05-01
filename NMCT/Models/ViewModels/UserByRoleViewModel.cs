using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMCT.Models.ViewModels
{
    public class UserByRoleViewModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }
}