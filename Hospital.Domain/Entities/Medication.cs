using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain.Entities
{
    [Table("MEDICATION")]
    public class Medication : BaseEntity
    {
        [Key]
        public int MedicationID { get; set; }

        [Required, MaxLength(100)]
        public string MedicationName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? GenericName { get; set; }

        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? Unit { get; set; } // Viên, Vỉ, Hộp
    }
}