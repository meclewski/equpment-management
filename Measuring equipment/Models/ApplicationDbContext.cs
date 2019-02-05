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
        public DbSet<Place> Places { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity(typeof(Device))
                        .HasOne("Measuring_equipment.Models.Place", "Place")
                        .WithMany("Devices")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Restrict);
            modelbuilder.Entity(typeof(Device))
                        .HasOne("Measuring_equipment.Models.Type", "Type")
                        .WithMany("Devices")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict);
            modelbuilder.Entity(typeof(Place))
                        .HasOne("Measuring_equipment.Models.Department", "Department")
                        .WithMany("Places")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);
            modelbuilder.Entity(typeof(Type))
                        .HasOne("Measuring_equipment.Models.Laboratory", "Laboratory")
                        .WithMany("Types")
                        .HasForeignKey("LaboratoryId")
                        .OnDelete(DeleteBehavior.Restrict);
            modelbuilder.Entity(typeof(Type))
                        .HasOne("Measuring_equipment.Models.Producer", "Producer")
                        .WithMany("Types")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Restrict);
            modelbuilder.Entity(typeof(Type))
                        .HasOne("Measuring_equipment.Models.Verification", "Verification")
                        .WithMany("Types")
                        .HasForeignKey("VerificationId")
                        .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
