namespace $safeprojectname$.Parameters
{
    // Represents a parameter for filtering and sorting lists of data.
    public class ListParameter
    {
        // Gets or sets the name of the property to use when ordering the list.
        public virtual string OrderBy { get; set; }
    }
}