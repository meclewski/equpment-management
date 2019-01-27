using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Measuring_equipment.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Verification> Verification { get; set; }
        
    }
}
