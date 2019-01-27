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
            .Include(x => x.Type);
        public IQueryable<Device> DevicesDT => context.Devices;
        public IQueryable<Producer> Producers => context.Producers;
        public IQueryable<Type> Types => context.Types;
        public IQueryable<Verification> Verifications => context.Verification;

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
                context.SaveChanges();
            }
            return dbEntry;
        }


    }
}
