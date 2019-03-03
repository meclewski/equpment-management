using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Measuring_equipment.Models
{
    public class EFDeviceRepository : IDeviceRepository
    {
        private ApplicationDbContext context;

        public EFDeviceRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Device> Devices => context.Devices
            .Include(x => x.Type).ThenInclude(v => v.Verification)
            .Include(x => x.Type).ThenInclude(x => x.Producer)
            .Include(x => x.Type).ThenInclude(x => x.Laboratory)
            .Include(x => x.Place).ThenInclude(x => x.Department);

        public IQueryable<Device> DevicesDT => context.Devices;
       
        public IQueryable<Type> Types => context.Types
            .Include(x => x.Producer)
            .Include(x => x.Verification)
            .Include(x => x.Laboratory);
       
        public IQueryable<Place> Places => context.Places
            .Include(x => x.Department);

        public void SaveDevice(Device device)
        {
            if (device.CurrentlyInUse)
            {
                Type type = context.Types.First(t => t.TypeId == device.TypeId);
                int vp = type.ValidityPierod;

                DateTime dt = device.VerificationDate ?? DateTime.Today;
                device.TimeToVerification = dt.AddMonths(vp).AddDays(-1);
            }
            
            
            if (device.DeviceId == 0)
            {
                context.Devices.Add(device);
            }
            else
            {
                Device dbEntry = context.Devices.FirstOrDefault(d => d.DeviceId == device.DeviceId);
                if(dbEntry != null)
                {
                    dbEntry.InventoryNo = device.InventoryNo;
                    dbEntry.SerialNo = device.SerialNo;
                    dbEntry.VerificationDate = device.VerificationDate;
                    dbEntry.TimeToVerification = device.TimeToVerification;
                    dbEntry.VerificationResult = device.VerificationResult;
                    dbEntry.ProductionDate = device.ProductionDate;
                    dbEntry.DeviceDesc = device.DeviceDesc;
                    dbEntry.TypeId = device.TypeId;
                    dbEntry.RegistrationNo = device.RegistrationNo;
                    dbEntry.CurrentlyInUse = device.CurrentlyInUse;
                    dbEntry.PlaceId = device.PlaceId;
                    dbEntry.UserId = device.UserId;
                }
            }
            context.SaveChanges();
        }

        
        public async Task<Device> DeleteDeviceAsync(int deviceId)
        {
            Device dbEntry = await context.Devices.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            if (dbEntry != null)
            {
                context.Devices.Remove(dbEntry);
                await context.SaveChangesAsync();
            }
            return dbEntry;
        }

        
    }
}
