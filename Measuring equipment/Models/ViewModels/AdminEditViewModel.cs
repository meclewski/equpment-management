using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace Measuring_equipment.Models.ViewModels
{
    public class AdminEditViewModel
    {
        
        public int DeviceId { get; set; }
        [Required(ErrorMessage = "Proszę podać numer kalibracji.")]
        public int RegistrationNo { get; set; }
        [StringLength(10, ErrorMessage = "Nazwa może zawierać max. 10 znaków. ")]
        public string InventoryNo { get; set; }
        [StringLength(30, ErrorMessage = "Nazwa może zawierać max. 30 znaków. ")]
        public string SerialNo { get; set; }
        [Required(ErrorMessage = "Proszę podać datę weryfikacji.")]
        [DataType(DataType.Date)]
        public DateTime? VerificationDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime? TimeToVerification { get; set; }
        [Required(ErrorMessage = "Proszę podać wynik weryfikacji.")]
        public string VerificationResult { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ProductionDate { get; set; }
        public string DeviceDesc { get; set; }
        [Required(ErrorMessage = "Proszę określić czy urządzenie jest używane.")]
        public bool CurrentlyInUse { get; set; } = true;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int TypeId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int PlaceId { get; set; }
        public string DeviceName { get; set; }
        public string ProducerName { get; set; }
        public string VerificationName { get; set; }
        public string ImageStr { get; set; }
        public List<SelectListItem> TypeListVm { get; set; }
        public List<SelectListItem> PlaceListVm { get; set; }
        public List<SelectListItem> UserListVm { get; set; }
        public string DepartmentName { get; set; }
        public string UserId { get; set; }
    }
}
