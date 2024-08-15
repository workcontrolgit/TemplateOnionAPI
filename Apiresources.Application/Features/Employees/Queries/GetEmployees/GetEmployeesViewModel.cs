using $ext_projectname$.Domain.Entities;
using $ext_projectname$.Domain.Enums;
using System;

namespace $safeprojectname$.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string EmployeeNumber { get; set; }
        public string Prefix { get; set; }
        public string Phone { get; set; }
        public virtual Position Position { get; set; }
        public decimal Salary { get; set; }

    }
}