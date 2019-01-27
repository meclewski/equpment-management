using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Measuring_equipment.Models
{
    public interface IDeviceRepository
    {
        IQueryable<Device> Devices { get; }
        IQueryable<Device> DevicesDT { get; }
        IQueryable<Type> Types { get; }
        IQueryable<Producer> Producers { get; }
        IQueryable<Verification> Verifications { get; }

        void SaveDevice(Device device);
        
        Task<Device> DeleteDeviceAsync(int deviceId);
       
        
    }
}
