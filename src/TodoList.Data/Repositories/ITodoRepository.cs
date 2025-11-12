using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Entities;
using TodoList.Shared;

namespace TodoList.Data.Repositories
{
    public interface ITodoRepository : IRepositoryAsync<Todo>
    {
        Task<(List<Todo> filteredTodos, int totalCount)> GetFilteredTodosAsync(QueryParameters queryParameters, string? userEmail = null);
        Task<Todo?> GetAsync(int id, string userEmail);
    }
}
