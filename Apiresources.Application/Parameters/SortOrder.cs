namespace $safeprojectname$.Parameters
{
    // Represents the sort order for a column in a data grid.
    public class SortOrder
    {
        // The index of the column to sort.
        public int Column { get; set; }

        // The direction to sort, either "asc" or "desc".
        public string Dir { get; set; }
    }
}