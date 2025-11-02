using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Contexts;
using TodoList.Data.Entities;
using System.Reflection;
using TodoList.Shared;

namespace TodoList.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationContext _context;

        public TodoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Todo entity)
        {
            await _context.Todos.AddAsync(entity);
        }

        public async Task<Todo?> GetAsync(int id)
        {
            return await _context.Todos.Include(x => x.User).FirstOrDefaultAsync();
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            return await _context.Todos.Include(x => x.User).ToListAsync();
        }

        public async Task<(List<Todo> filteredTodos, int totalCount)> GetFilteredTodosAsync(QueryParameters queryParameters)
        {
            IQueryable<Todo> filteredTodos = _context.Todos.Include(x => x.User);

            //SearchTerm. Filtering by search term
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                filteredTodos = filteredTodos.Where(x => x.Title.ToLower().Contains(queryParameters.SearchTerm!.ToLower()));
            }

            int totalCount = await filteredTodos.CountAsync();

            //SortBy. Sorting
            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
            {
                switch (queryParameters.SortBy.ToLower())
                {
                    case "title":
                        filteredTodos = queryParameters.SortDesc ? filteredTodos.OrderByDescending(x => x.Title) : filteredTodos.OrderBy(x => x.Title);
                        break;
                    case "description":
                        filteredTodos = queryParameters.SortDesc ? filteredTodos.OrderByDescending(x => x.Description) : filteredTodos.OrderBy(x => x.Description);
                        break;
                    case "createdat":
                        filteredTodos = queryParameters.SortDesc ? filteredTodos.OrderByDescending(x => x.CreatedAt) : filteredTodos.OrderBy(x => x.CreatedAt);
                        break;
                    case "updatedat":
                        filteredTodos = queryParameters.SortDesc ? filteredTodos.OrderByDescending(x => x.UpdatedAt) : filteredTodos.OrderBy(x => x.UpdatedAt);
                        break;
                    default:
                        filteredTodos = queryParameters.SortDesc ? filteredTodos.OrderByDescending(x => x.Id) : filteredTodos.OrderBy(x => x.Id);
                        break;
                }
            }
            else
            {
                filteredTodos = queryParameters.SortDesc ? filteredTodos.OrderByDescending(x => x.Id) : filteredTodos.OrderBy(x => x.Id);
            }

            // Pagination
            int skip = (queryParameters.Page - 1) * queryParameters.Limit;
            filteredTodos = filteredTodos.Skip(skip).Take(queryParameters.Limit);

            return (await filteredTodos.ToListAsync(), totalCount);
        }


        public async Task UpdateAsync(Todo entity)
        {
            await Task.Run(() =>
            {
                entity.UpdatedAt = DateTime.UtcNow;
                _context.Todos.Update(entity);
            });
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await this.GetAsync(id);

            if (todo != null)
            {
                _context.Todos.Remove(todo);
            }
        }

        public async Task DeleteAsync(Todo entity)
        {
            await Task.Run(() =>
            {
                _context.Todos.Remove(entity);
            });
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Todos.AnyAsync(x => x.Id == id);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
