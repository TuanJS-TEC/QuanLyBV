using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain.Entities
{
    [Table("DEPARTMENTS")]
    public class Department : BaseEntity
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required] [MaxLength(100)] public string DepartmentName { get; set; } = string.Empty;
        [Required] [MaxLength(20)] public string DepartmentCode { get; set; } = string.Empty;
        [MaxLength(500)] public string? Description { get; set; }
        [MaxLength(100)] public string Location { get; set; } = string.Empty;
        [MaxLength(10)] public string? Phone { get; set; } 
        [MaxLength(100)] public string? HeadOfDepartment { get; set; }
    }
};

