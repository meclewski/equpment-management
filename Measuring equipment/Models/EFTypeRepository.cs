using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Measuring_equipment.Models
{
    public class EFTypeRepository : ITypeRepository
    {
        private ApplicationDbContext context;
        public EFTypeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Type> Types => context.Types
            .Include(p => p.Producer)
            .Include(v => v.Verification)
            .Include(l => l.Laboratory);

        public IQueryable<Type> TypesDT => context.Types;

        

        public void SaveType(Type type)
        {
            
            if (type.TypeId == 0)
            {
                context.Types.Add(type);
            }
            else
            {
                Type dbEntry = context.Types.FirstOrDefault(d => d.TypeId == type.TypeId);
                if (dbEntry != null)
                {
                    dbEntry.TypeName = type.TypeName;
                    dbEntry.DeviceName = type.DeviceName;
                    dbEntry.ValidityPierod = type.ValidityPierod;
                    dbEntry.Price = type.Price;
                    dbEntry.Image = type.Image;
                    dbEntry.TypeDesc = type.TypeDesc;
                    dbEntry.VerificationId = type.VerificationId;
                    dbEntry.ProducerId = type.ProducerId;
                    dbEntry.LaboratoryId = type.LaboratoryId;
                }
            }
            context.SaveChanges();
        }

        public async Task<Type> DeleteTypeAsync(int typeId)
        {
            Type dbEntry = await context.Types.FirstOrDefaultAsync(d => d.TypeId == typeId);
            if (dbEntry != null)
            {
                context.Types.Remove(dbEntry);
                await context.SaveChangesAsync();
            }
            return dbEntry;
        }

        
    }
}
