namespace $safeprojectname$.Enums
{
    /// <summary>
    /// Defines different roles within an organization.
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// The highest level of access and control, with full responsibility for the entire organization.
        /// </summary>
        SuperAdmin,

        /// <summary>
        /// A high-level position responsible for managing a specific department or team within the organization.
        /// </summary>
        Admin,

        /// <summary>
        /// A mid-level position responsible for supervising and managing a team of employees within the organization.
        /// </summary>
        Manager,

        /// <summary>
        /// A low-level position responsible for performing daily tasks related to their specific job function within the organization.
        /// </summary>
        Employee
    }
}