using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Measuring_equipment.Models
{
    
    public class Device
    {
        public int DeviceId { get; set; }
        [Required(ErrorMessage = "Proszę podać numer kalibracji.")]
        public int RegistrationNo { get; set; }
        [StringLength(10, ErrorMessage = "Nazwa może zawierać max. 10 znaków. ")]
        public string InventoryNo { get; set; }
        [StringLength(30, ErrorMessage = "Nazwa może zawierać max. 30 znaków. ")]
        public string SerialNo { get; set; }
        //[Required(ErrorMessage = "Proszę podać datę weryfikacji.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? VerificationDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? TimeToVerification { get; set; }
        [Required(ErrorMessage = "Proszę podać wynik weryfikacji.")]
        public string VerificationResult { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? ProductionDate { get; set; }
        public string DeviceDesc { get; set; }
        [Required(ErrorMessage = "Proszę określić czy urządzenie jest używane.")]
        public bool CurrentlyInUse { get; set; } = true;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać wartość")]
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public string UserId { get; set; }
    }
}
