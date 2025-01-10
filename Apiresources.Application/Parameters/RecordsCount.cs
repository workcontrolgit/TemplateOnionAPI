namespace $safeprojectname$.Parameters
{
    /// <summary>
    /// Represents a class containing records count information.
    /// </summary>
    public class RecordsCount
    {
        /// <summary>
        /// Gets or sets the number of filtered records.
        /// </summary>
        public int RecordsFiltered { get; set; }
        /// <summary>
        /// Gets or sets the total number of records.
        /// </summary>
        public int RecordsTotal { get; set; }
    }
}