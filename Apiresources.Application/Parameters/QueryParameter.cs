namespace $safeprojectname$.Parameters
{
    public class QueryParameter : PagingParameter
    {
        public virtual string OrderBy { get; set; }
        public virtual string Fields { get; set; }
    }
}