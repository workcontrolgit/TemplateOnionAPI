using $safeprojectname$.Common;
using $safeprojectname$.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Entities
{
    public class Employee : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        // Foreign Key for Position
        public Guid PositionId { get; set; }

        // Navigation Property for Position
        public virtual Position Position { get; set; }

        // Salary of the Employee
        public decimal Salary { get; set; }

        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string EmployeeNumber { get; set; }
        public string Prefix { get; set; }
        public string Phone { get; set; }
    }
}