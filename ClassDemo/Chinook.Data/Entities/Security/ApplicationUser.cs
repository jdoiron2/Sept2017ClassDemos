using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Data.Entities.Security
{
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeId { get; set; }
        public int? CustomerID { get; set; }
    }
}
