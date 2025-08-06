using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.Data.Entities;
using TodoList.Data.Repositories;
using TodoList.Shared;
using TodoList.Shared.Todo;

namespace TodoList.Server.Services.TodoService
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<TodoResponse>> GetTodoAsync(int id)
        {
            var todo = await _todoRepository.GetAsync(id);
            var todoResponse = _mapper.Map<TodoResponse>(todo);

            var serviceResult = new ServiceResult<TodoResponse>()
            {
                Data = todoResponse,
                Success = todoResponse != null,
                Message = todoResponse != null ? "Todo retrieved" : "Todo not found",
                StatusCode = todoResponse != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound
            };

            return serviceResult;
        }

        public Task<ServiceResult<List<TodoResponse>>> GetTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TodoResponse>> CreateTodoAsync(CreateTodoRequest createTodoRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool?>> UpdateTodoAsync(UpdateTodoRequest updateTodoRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool?>> DeleteTodoAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
