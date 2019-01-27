using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public class FakeDeviceRepository /*: IDeviceRepository */
    {
        public IQueryable<Device> Devices => new List<Device> {
            new Device {InventoryNo = "TPVD000001", SerialNo = "SN00001", DeviceDesc = "Test1"},
            new Device {InventoryNo = "TPVD000002", SerialNo = "SN00002", DeviceDesc = "Test2"},
            new Device {InventoryNo = "TPVD000003", SerialNo = "SN00003", DeviceDesc = "Test3"}
        }.AsQueryable<Device>();

        
    }
}
