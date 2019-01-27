using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Measuring_equipment.Models
{
    public static class SeedDataType
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            context.Devices.Include(b => b.Type);
            if (!context.Devices.Any())
            {

                context.Types.AddRange(
                    new Type
                    {
                        TypeName = "TypeName1",
                        ValidityPierod = 2,
                        TypeDesc = "TypeTest1"


                    },
                     new Type
                     {
                         TypeName = "TypeName2",
                         ValidityPierod = 3,
                         TypeDesc = "TypeTest2"


                     }

                    );

                context.SaveChanges();
            }
        }
    }
}
          