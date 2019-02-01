using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Measuring_equipment.Models
{
    public class Type
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string DeviceName { get; set; }
        public int ValidityPierod { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public string TypeDesc { get; set; }
        public int ProducerId { get; set; }  
        public int LaboratoryId { get; set; }
        public int VerificationId { get; set; }
        public virtual Verification Verification { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual Laboratory Laboratory { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
