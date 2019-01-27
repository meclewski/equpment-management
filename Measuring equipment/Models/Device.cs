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
        public string InventoryNo { get; set; }
        public string SerialNo { get; set; }
        [Required(ErrorMessage = "Proszę podać datę weryfikacji.")]
        public DateTime? VerificationDate { get; set; } = DateTime.Now;
        public DateTime? TimeToVerification { get; set; }
        [Required(ErrorMessage = "Proszę podać wynik weryfikacji.")]
        public string VerificationResult { get; set; }
        public DateTime? ProductionDate { get; set; }
        public string DeviceDesc { get; set; }
        [Required(ErrorMessage = "Proszę określić czy urządzenie jest używane.")]
        public bool CurrentlyInUse { get; set; } = true;
        [Required(ErrorMessage = "Proszę podać typ urządzenia") ]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
    }
}
