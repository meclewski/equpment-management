using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Measuring_equipment.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            //context.Devices.Include(b => b.Type);

            /*
            foreach (var d in context.Devices) {
                d.DeviceName = "Nazwa";
            }
            */
            context.SaveChanges();
            

            if (!context.Devices.Any())
            {

                context.Devices.AddRange(
                    new Device
                    {
                        InventoryNo = "TPVD000001",
                        SerialNo = "SN00001",
                        DeviceDesc = "Test1",
                        TypeId = 1
                    },
                    new Device
                    {
                        InventoryNo = "TPVD000002",
                        SerialNo = "SN00002",
                        DeviceDesc = "Test2",
                        TypeId = 1
                    }



                    );
                context.SaveChanges();
            }
            
        }
    }
}
