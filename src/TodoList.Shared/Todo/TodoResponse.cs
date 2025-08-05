using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Shared.Todo
{
    public class TodoResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public required string Description { get; set; }
    }
}
