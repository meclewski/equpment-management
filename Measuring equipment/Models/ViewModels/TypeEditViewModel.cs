using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Measuring_equipment.Models.ViewModels
{
    public class TypeEditViewModel
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
        [Range(1, int.MaxValue, ErrorMessage="Proszę wybrać wartość")]
        public int ProducerId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int LaboratoryId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int VerificationId { get; set; }
        public string ImageStr { get; set; }
        public List<SelectListItem> ProducerListVm { get; set; }
        public List<SelectListItem> LaboratoryListVm { get; set; }
        public List<SelectListItem> VerificationListVm { get; set; }
        public string ReturnUrl { get; set; }
    }
}
