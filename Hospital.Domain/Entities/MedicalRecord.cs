using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain.Entities
{
    [Table("MEDICAL_RECORD")]
    public class MedicalRecord : BaseEntity
    {
        [Key]
        public int RecordID { get; set; }

        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        
        public int? AppointmentID { get; set; }

        public DateTime VisitDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? ChiefComplaint { get; set; } 

        public string? Symptoms { get; set; }
        public string? Examination { get; set; }

        public decimal? Temperature { get; set; }
        public int? BloodPressureSystolic { get; set; }
        public int? BloodPressureDiastolic { get; set; } 
        public int? HeartRate { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }

        public string? Notes { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }

        [ForeignKey("AppointmentID")]
        public virtual Appointment? Appointment { get; set; }
    }
}