using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Entities
{
    [Table("PRESCRIPTION_DETAIL")]
    public class PrescriptionDetail
    {
        [Key]
        public int PrescriptionDetailID { get; set; }

        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }

        public int Quantity { get; set; }
        
        [MaxLength(50)]
        public string? Dosage { get; set; }
        
        [MaxLength(50)]
        public string? Frequency { get; set; }
        
        public int? Duration { get; set; }
        
        [MaxLength(200)]
        public string? Instructions { get; set; }

        // Navigation
        [ForeignKey("PrescriptionID")]
        public virtual Prescription? Prescription { get; set; }

        [ForeignKey("MedicationID")]
        public virtual Medication? Medication { get; set; }
    }
}