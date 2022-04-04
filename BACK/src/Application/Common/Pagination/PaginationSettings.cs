using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Pagination
{
    public static class PaginationSettings
    {
        public static string SetOrder(string value)
        {
            return (value.ToLower() == "asc" || value.ToLower() == "desc") ? value : "asc";
        }
    }
}
