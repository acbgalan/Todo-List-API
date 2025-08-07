using TodoList.Shared;
using TodoList.Shared.Todo;

namespace TodoList.Server.Services.TodoService
{
    public interface ITodoService
    {
        Task<ServiceResult<TodoResponse>> GetTodoAsync(int id);
        Task<ServiceResult<List<TodoResponse>>> GetTodosAsync();
        Task<ServiceResult<TodoResponse>> CreateTodoAsync(CreateTodoRequest createTodoRequest);
        Task<ServiceResult<bool?>> UpdateTodoAsync(UpdateTodoRequest updateTodoRequest);
        Task<ServiceResult<bool?>> DeleteTodoAsync(int id);
    }
}
