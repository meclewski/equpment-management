using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Measuring_equipment.Models
{
    
    public class Device
    {
        public int DeviceId { get; set; }
        public int RegistrationNo { get; set; }
        public string InventoryNo { get; set; }
        public string SerialNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? VerificationDate { get; set; } = DateTime.Now;
        public DateTime? TimeToVerification { get; set; }
        public string VerificationResult { get; set; }
        public DateTime? ProductionDate { get; set; }
        public string DeviceDesc { get; set; }
        public bool CurrentlyInUse { get; set; } = true;
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public string UserId { get; set; }
    }
}
