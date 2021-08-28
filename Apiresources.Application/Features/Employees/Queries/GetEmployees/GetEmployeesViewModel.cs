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
        public string EmployeeTitle { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string EmployeeNumber { get; set; }
        public string Suffix { get; set; }
        public string Phone { get; set; }
    }
}