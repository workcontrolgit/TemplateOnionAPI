namespace $safeprojectname$.Features.Employees.Queries.GetEmployees
{
    /// <summary>
    /// Represents a view model for the GetEmployees query.
    /// </summary>
    public class GetEmployeesViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the employee.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name of the employee.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birthday of the employee.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the gender of the employee.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the employee number.
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// Gets or sets the prefix of the employee's name.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the employee.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the position held by the employee.
        /// </summary>
        public virtual Position Position { get; set; }

        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        public decimal Salary { get; set; }
    }
}