using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PensionManagementSystem.DTOs;
using PensionManagementSystem.Models;

namespace PensionManagementSystem.Data
{
    public class PensionManagementSystemContext : DbContext
    {
        public PensionManagementSystemContext (DbContextOptions<PensionManagementSystemContext> options)
            : base(options)
        {
        }
        public DbSet<PensionManagementSystem.Models.PensionScheme>? PensionScheme { get; set; }
        public DbSet<PensionManagementSystem.Models.PensionStatus>? PensionStatus { get; set; }
        public DbSet<PensionManagementSystem.Models.Request>? Request { get; set; }
        public DbSet<PensionManagementSystem.Models.Login>? Login { get; set; }
        public DbSet<PensionManagementSystem.Models.User>? User { get; set; }








    }
}
