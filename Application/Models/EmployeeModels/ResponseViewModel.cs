using System.Net;

namespace Application.Models.EmployeeModels
{
    public class ResponseViewModel<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string? Message { get; set; } = null;
        public string? Error { get; set; } = null;
        public T? Data { get; set; }

        public int? TotalCount { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public bool IsSuccess => (int)StatusCode >= 200 && (int)StatusCode < 300;
    }
}
