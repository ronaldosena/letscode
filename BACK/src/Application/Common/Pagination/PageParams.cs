using System.Text.Json.Serialization;

namespace Application.Common.Pagination
{
    public class PageParams
    {
        private const int maxPageSize = 5;
        private int page = 1;
        private int limit = 2;

        [JsonPropertyName("page")]
        public int Page { get => page <= 0 ? 1 : page; set => page = value; }

        [JsonPropertyName("limit")]
        public int Limit { get => limit <= 0 ? 1 : (limit > maxPageSize ? maxPageSize : limit); set => limit = value; }

        public int Skip()
        {
            return (Page - 1) * Limit;
        }

        // public int Limit { get => limit <= 0 ? 1 : ReturnSize(limit); set => limit = value; }
        // private int ReturnSize(int size)
        // {
        //     return size > maxPageSize ? maxPageSize : size;
        // }
    }
}
