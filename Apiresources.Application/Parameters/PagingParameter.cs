namespace $safeprojectname$.Parameters
{
    // Defines a class for paging parameters that can be used to control how data is paginated.
    public class PagingParameter
    {
        // Maximum page size allowed (set to 200).
        private const int maxPageSize = 200;

        // Gets or sets the current page number, defaulting to 1 if not provided.
        public int PageNumber { get; set; } = 1;

        // Gets or sets the current page size. If a value greater than the maximum page size is provided,
        // it will be limited to the maximum page size instead of throwing an error.
        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}