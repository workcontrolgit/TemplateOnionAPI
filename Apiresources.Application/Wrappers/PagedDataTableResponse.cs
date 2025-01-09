namespace $safeprojectname$.Wrappers
{
    // A response object that includes data and paged information for a DataTable.
    public class PagedDataTableResponse<T> : Response<T>
    {
        // The number of times the request has been processed.
        public int Draw { get; set; }
        
        // The number of records filtered based on search criteria.
        public int RecordsFiltered { get; set; }
        
        // The total number of records in the table before filtering.
        public int RecordsTotal { get; set; }

        // Constructor for PagedDataTableResponse object. Initializes the response with data, page number, and record counts.
        public PagedDataTableResponse(T data, int pageNumber, RecordsCount recordsCount)
        {
            this.Draw = pageNumber;
            this.RecordsFiltered = recordsCount.RecordsFiltered;
            this.RecordsTotal = recordsCount.RecordsTotal;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}