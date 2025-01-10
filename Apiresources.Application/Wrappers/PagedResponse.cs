namespace $safeprojectname$.Wrappers
{
    // PagedResponse class that inherits from Response<T> class and represents a paged response with data and pagination information.
    public class PagedResponse<T> : Response<T>
    {
        // Gets or sets the current page number of the response.
        public virtual int PageNumber { get; set; }

        // Gets the size of each page in the response.
        public int PageSize { get; set; }

        // Gets the total number of records that were filtered based on some criteria.
        public int RecordsFiltered { get; set; }

        // Gets the total number of records available before any filtering was applied.
        public int RecordsTotal { get; set; }

        // Initializes a new instance of the PagedResponse class with the specified data and pagination information.
        public PagedResponse(T data, int pageNumber, int pageSize, RecordsCount recordsCount)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.RecordsFiltered = recordsCount.RecordsFiltered;
            this.RecordsTotal = recordsCount.RecordsTotal;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}