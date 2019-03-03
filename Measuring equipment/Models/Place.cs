using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public class Place
    {
        public int PlaceId { get; set; }
        [Required(ErrorMessage = "Proszę podać nazwę miejsca.")]
        public string PlaceName { get; set; }
        public string PlaceDesc { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
