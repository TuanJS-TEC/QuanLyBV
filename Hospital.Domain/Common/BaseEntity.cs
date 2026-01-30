using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Common
{
    public abstract class BaseEntity
    {
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Column("isActive")] public bool IsActive { get; set; } = true;
    }
};

