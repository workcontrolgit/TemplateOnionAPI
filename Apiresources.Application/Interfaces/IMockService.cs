namespace $safeprojectname$.Interfaces
{
    /// <summary>
    /// Defines a mock service for generating data.
    /// </summary>
    public interface IMockService
    {
        /// <summary>
        /// Generates positions based on the provided parameters.
        /// </summary>
        /// <param name="rowCount">Number of rows to generate.</param>
        /// <param name="departments">Departments to include in generated data.</param>
        /// <param name="salaryRanges">Salary ranges to include in generated data.</param>
        /// <returns>A list of positions.</returns>
        List<Position> GetPositions(int rowCount, IEnumerable<Department> departments, IEnumerable<SalaryRange> salaryRanges);

        /// <summary>
        /// Generates employees based on the provided parameters.
        /// </summary>
        /// <param name="rowCount">Number of rows to generate.</param>
        /// <returns>A list of employees.</returns>
        List<Employee> GetEmployees(int rowCount);

        /// <summary>
        /// Seeds positions with predefined data.
        /// </summary>
        /// <param name="rowCount">Number of rows to seed.</param>
        /// <returns>A list of seeded positions.</returns>
        List<Position> SeedPositions(int rowCount);
    }
}