using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain.Entities
{
    [Table("DOCTOR")]
    public class Doctor : BaseEntity
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Specialization { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public int DepartmentID { get; set; }
        
        [ForeignKey("DepartmentID")]
        public virtual Department? Department { get; set; }

        public decimal? ConsultationFee { get; set; }
        
        public int? ExperienceYears { get; set; }
        
        [MaxLength(200)]
        public string? Qualification { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}