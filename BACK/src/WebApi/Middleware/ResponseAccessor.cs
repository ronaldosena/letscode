using System.Text.Json;
using Application.Common.Interfaces;
using Application.Common.Pagination;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middleware
{
    public class ResponseAccessor : IResponseAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ResponseAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void AddPagination(PaginationHeader header)
        {
            httpContextAccessor.HttpContext.Response.Headers.Add("Pagination-Count", header.TotalItems.ToString());
            httpContextAccessor.HttpContext.Response.Headers.Add("Pagination-Limit", header.ItemsPerPage.ToString());
            httpContextAccessor.HttpContext.Response.Headers.Add("Pagination-Page", header.CurrentPage.ToString());
        }

        public void AddToResponse(string name, object content)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            httpContextAccessor.HttpContext.Response.Headers.Add(name, JsonSerializer.Serialize(content, options));
        }
    }
}
