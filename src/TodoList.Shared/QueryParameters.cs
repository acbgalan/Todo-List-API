using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Shared
{
    public class QueryParameters
    {
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool SortDesc { get; set; } = false;
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
