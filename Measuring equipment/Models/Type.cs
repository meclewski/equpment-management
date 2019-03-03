using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Measuring_equipment.Models
{
    public class Type
    {
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Proszę podać nazwę typu.")]
        public string TypeName { get; set; }
        [Required(ErrorMessage = "Proszę podać nazwę urządzenia.")]
        [StringLength(100, ErrorMessage = "Nazwa może zawierać max. 100 znaków. ")]
        public string DeviceName { get; set; }
        [Required(ErrorMessage = "Proszę podać czasokres wzorcowania.")]
        public int ValidityPierod { get; set; }
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public string TypeDesc { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int ProducerId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int LaboratoryId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int VerificationId { get; set; }
        public virtual Verification Verification { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual Laboratory Laboratory { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
