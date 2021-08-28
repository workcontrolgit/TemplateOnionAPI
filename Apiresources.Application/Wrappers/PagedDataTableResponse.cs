using $safeprojectname$.Parameters;

namespace $safeprojectname$.Wrappers
{
    public class PagedDataTableResponse<T> : Response<T>
    {
        public int Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }

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