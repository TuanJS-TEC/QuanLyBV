using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain.Entities
{
    [Table("PRESCRIPTION")]
    public class Prescription : BaseEntity
    {
        [Key]
        public int PrescriptionID { get; set; }

        public int RecordID { get; set; } // Liên kết với Hồ sơ bệnh án
        public int DoctorID { get; set; } // Bác sĩ kê đơn

        public DateTime PrescriptionDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Notes { get; set; } // Lời dặn của bác sĩ

        // Navigation Properties
        [ForeignKey("RecordID")]
        public virtual MedicalRecord? MedicalRecord { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }

        // Quan hệ 1-N: 1 Đơn thuốc có nhiều Chi tiết
        public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
    }
}