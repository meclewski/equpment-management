using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public class Verification
    {
        public int VerificationId { get; set; }
        public string VerificationName { get; set; }
        public string VerificationDesc { get; set; }
        public virtual ICollection<Type> Types { get; set; }
    }
}
