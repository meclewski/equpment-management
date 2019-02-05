using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public class Place
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceDesc { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
