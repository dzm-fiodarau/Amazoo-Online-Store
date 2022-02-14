using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmazooApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AmazooApp.Data
{
   // public class AmazooAppDbContext : DbContext
          public class AmazooAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AmazooAppDbContext(DbContextOptions<AmazooAppDbContext> options) : base(options)
        {

        }
    }
}
