using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain.Entities
{
    [Table("APPOINTMENT")]
    public class Appointment : BaseEntity
    {
        [Key]
        public int AppointmentID { get; set; }

        [Required]
        public int PatientID { get; set; }
        
        [Required]
        public int DoctorID { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; } 

        public int Duration { get; set; } = 30;

        [MaxLength(50)]
        public string? AppointmentType { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Scheduled"; 

        [MaxLength(200)]
        public string? Reason { get; set; }

        public int? RoomID { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public bool IsCancelled { get; set; } = false;

        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }
    }
}