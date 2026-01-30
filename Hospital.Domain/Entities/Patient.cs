using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain.Entities
{
    [Table("PATIENT")]
    public class Patient : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }
        
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [MaxLength(10)]
        public string? Gender { get; set; } = string.Empty;
        
        [Required]
        public string? BloodGroup { get; set; }
        
        [MaxLength(15)]
        public string? Phone { get; set; }
        
        [MaxLength(200)]
        public string? Email { get; set; }
        
        [MaxLength(200)]
        public string? Address { get; set; }
        
        [MaxLength(50)]
        public string? City { get; set; }
        
        [MaxLength(50)]
        public string? EmergencyContact { get; set; }
        
        [MaxLength(20)]
        public string? EmergencyPhone { get; set; }
        
        public int? InsuranceID { get; set; }
        
        // public virtual Insurance? Insurance { get; set; }
    }
};

