using TodoList.Shared;
using TodoList.Shared.Todo;

namespace TodoList.Server.Services.TodoService
{
    public class TodoService : ITodoService
    {
        public Task<ServiceResult<TodoResponse>> CreateTodoAsync(CreateTodoRequest createTodoRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool?>> DeleteTodoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TodoResponse>> GetTodoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<TodoResponse>>> GetTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool?>> UpdateTodoAsync(UpdateTodoRequest updateTodoRequest)
        {
            throw new NotImplementedException();
        }
    }
}
