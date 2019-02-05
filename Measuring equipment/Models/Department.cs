using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
