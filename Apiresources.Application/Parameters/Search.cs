// This namespace contains classes and other elements related to project parameters.
namespace $safeprojectname$.Parameters
{
    // The Search class represents search-related parameters.
    public class Search
    {
        // The Value property is a string representing the value of the search query.
        public string Value { get; set; }
        
        // The Regex property is a boolean indicating whether to use regular expressions for the search.
        public bool Regex { get; set; }
    }
}