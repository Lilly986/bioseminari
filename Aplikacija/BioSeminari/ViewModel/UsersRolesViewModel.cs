using Microsoft.AspNet.Identity.EntityFramework;
using MSDNRoles.Models;
using System.Collections.Generic;

namespace MSDNRoles.ViewModel
{
    public class UsersRolesViewModel
    {
        public List<ApplicationUser> Users { set; get; }
        public List<IdentityRole> Roles { set; get; }
    }
}