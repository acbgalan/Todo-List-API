using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Entities;
using TodoList.Shared.Todo;

namespace TodoList.Data.Repositories
{
    public interface ITodoRepository : IRepositoryAsync<Todo>
    {
        Task<List<Todo>> GetFilteredTodosAsync(QueryParametersTodo queryParameters);
    }
}
