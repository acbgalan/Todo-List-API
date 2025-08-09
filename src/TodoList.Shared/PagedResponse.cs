using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Shared
{
    public class PagedResponse<T>
    {
        public List<T>? Data { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}
