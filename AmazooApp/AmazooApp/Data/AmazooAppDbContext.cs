using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmazooApp.Models;

namespace AmazooApp.Data
{
    public class AmazooAppDbContext : DbContext
    {
        public AmazooAppDbContext(DbContextOptions<AmazooAppDbContext> options) : base(options)
        {

        }

    }
}
