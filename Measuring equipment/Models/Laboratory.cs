using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public class Laboratory
    {
        public int LaboratoryId { get; set; }
        public string LaboratoryName { get; set; }
        public string Accreditation { get; set; }
        public string LaboratoryDesc { get; set; }
        public virtual ICollection<Type> Types { get; set; }
    }
}
