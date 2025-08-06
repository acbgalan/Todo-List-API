using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ServiceResult<List<TodoResponse>>> GetTodosAsync()
        {
            var todos = await _todoRepository.GetAllAsync();
            var todosResponse = _mapper.Map<List<TodoResponse>>(todos);

            var serviceResult = new ServiceResult<List<TodoResponse>>()
            {
                Data = todosResponse,
                Success = todosResponse.Any(),
                Message = todosResponse.Any() ? "Todos retrieved" : "Todos not found",
                StatusCode = todosResponse.Any() ? StatusCodes.Status200OK : StatusCodes.Status404NotFound
            };

            return serviceResult;
        }

        public async Task<ServiceResult<TodoResponse>> CreateTodoAsync(CreateTodoRequest createTodoRequest)
        {
            var serviceResult = new ServiceResult<TodoResponse>();

            try
            {
                var todo = _mapper.Map<Todo>(createTodoRequest);
                await _todoRepository.AddAsync(todo);
                int saveResult = await _todoRepository.SaveAsync();
                var todoResponse = _mapper.Map<TodoResponse>(todo);

                serviceResult = new ServiceResult<TodoResponse>()
                {
                    Data = todoResponse,
                    Success = saveResult > 0,
                    Message = saveResult > 0 ? "Todo created successfully" : "Unexpected value when saving",
                    StatusCode = saveResult > 0 ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError
                };
            }
            catch (DbUpdateException ex)
            {
                serviceResult = new ServiceResult<TodoResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Database error: {ex.Message}",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            catch (Exception ex)
            {
                serviceResult = new ServiceResult<TodoResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return serviceResult;
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
