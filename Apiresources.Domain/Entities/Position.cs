using $safeprojectname$.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $safeprojectname$.Entities
{
    public class Position : AuditableBaseEntity
    {
        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string PositionTitle { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string PositionNumber { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public string PositionDescription { get; set; }
        public string PostionArea { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string PostionType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PositionSalary { get; set; }
    }
}