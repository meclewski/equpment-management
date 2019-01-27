using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
        public string ProducerDesc { get; set; }
        public virtual ICollection<Type> Types { get; set; }
    }
}
