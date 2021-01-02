namespace $safeprojectname$.Filters
{
    public class PagingParameter
    {
        const int maxPageSize = 200;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public PagingParameter()
        {
            //this.PageNumber = 1;
            //this.PageSize = 10;
        }
        //public PagingParameter(int pageNumber, int pageSize)
        //{
        //    this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        //    this.PageSize = pageSize > maxPageSize ? maxPageSize : pageSize;
        //}
    }
}
