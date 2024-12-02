namespace Application.Contract.Pagination
{
    public class PaginationResponse<TReturn>:Responses
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public int Sort { get; set; }
        public List<TReturn> Data { get; set; }
    }
    public class Responses
    {
        public bool Errored { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class SingleResponse<TReturn> : Responses
    {
        public TReturn Data { get; set; }
    }
}
