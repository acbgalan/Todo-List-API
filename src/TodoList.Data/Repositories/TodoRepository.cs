using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Contexts;
using TodoList.Data.Entities;

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
            return await _context.Todos.FindAsync(id);
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
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
