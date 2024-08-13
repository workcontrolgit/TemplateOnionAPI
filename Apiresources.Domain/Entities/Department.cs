using System.Collections.Generic;
using TalentManagementApi.Domain.Common;

namespace TalentManagementApi.Domain.Entities
{
    public class Department : AuditableBaseEntity
    {
        // Department Name
        public string Name { get; set; }

        // Navigation Property for related Positions
        public virtual ICollection<Position> Positions { get; set; }

        public Department()
        {
            Positions = new HashSet<Position>();
        }

        // Additional properties (e.g., Name, ManagerId, etc.) can be added here
    }
}