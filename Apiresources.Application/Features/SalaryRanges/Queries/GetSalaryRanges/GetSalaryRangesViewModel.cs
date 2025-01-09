// Define a namespace for the feature's queries and views
namespace $safeprojectname$.Features.SalaryRanges.Queries.GetSalaryRanges
{
    // Define a view model class to represent the salary range data
    public class GetSalaryRangesViewModel 
    {
        // Define properties for the ID, name, minimum and maximum salaries of the salary range
        /// <summary>
        /// The unique identifier for this salary range
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of this salary range
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The minimum salary in this range
        /// </summary>
        public decimal MinSalary { get; set; }

        /// <summary>
        /// The maximum salary in this range
        /// </summary>
        public decimal MaxSalary { get; set; }

    }
}