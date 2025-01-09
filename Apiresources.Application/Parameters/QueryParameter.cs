namespace $safeprojectname$.Parameters
{
    /// <summary>
    /// Represents a query parameter for filtering and sorting data.
    /// Inherits from PagingParameter to include pagination properties.
    /// </summary>
    public class QueryParameter : PagingParameter
    {
        /// <summary>
        /// Gets or sets the field name(s) to order the results by.
        /// </summary>
        public virtual string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the fields to include in the query results.
        /// </summary>
        public virtual string Fields { get; set; }
    }
}