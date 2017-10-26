using Chinook.Data.Entities.Security;
using ChinookSystem.DAL.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.BLL.Security
{
    public class RoleManager:RoleManager<IdentityRole>
    {
        public RoleManager() : base(new RoleStore<IdentityRole>
            (new ApplicationDbContext()))
        {
        
        }
        public void AddDefaultRoles()
        {
            foreach (string roleName in SecurityRoles.DefaultSecurityRoles)
            {
                // Check if it exists
                if (!Roles.Any(r => r.Name == roleName))
                {
                    this.Create(new IdentityRole(roleName));
                }
            }
        }
    }
}
