using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.Data.Entities;
using TodoList.Data.Repositories;
using TodoList.Server.Services.UserService;
using TodoList.Shared;
using TodoList.Shared.Todo;

namespace TodoList.Server.Services.TodoService
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private static readonly HashSet<string> validSortFields = new HashSet<string> { "id", "title", "description", "createdat", "updatedat" };


        public TodoService(ITodoRepository todoRepository, IMapper mapper, IUserService userService)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _userService = userService;
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

        public async Task<ServiceResult<PagedResponse<TodoResponse>>> GetTodosAsync(QueryParameters queryParameters)
        {
            //SortBy validation
            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy) && !validSortFields.Contains(queryParameters.SortBy))
            {
                return new ServiceResult<PagedResponse<TodoResponse>>
                {
                    Data = null,
                    Success = false,
                    Message = "Bad parameter",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            //Page and limit validation
            if (queryParameters.Page < 1 || queryParameters.Limit < 1)
            {
                return new ServiceResult<PagedResponse<TodoResponse>>
                {
                    Data = null,
                    Success = false,
                    Message = "Bad parameter",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var (filteredTodos, totalCount) = await _todoRepository.GetFilteredTodosAsync(queryParameters);

            var pagedResponse = new PagedResponse<TodoResponse>
            {
                Data = _mapper.Map<List<TodoResponse>>(filteredTodos),
                Page = queryParameters.Page,
                Limit = queryParameters.Limit,
                Total = totalCount
            };

            var serviceResult = new ServiceResult<PagedResponse<TodoResponse>>()
            {
                Data = pagedResponse,
                Success = pagedResponse.Data.Any(),
                Message = pagedResponse.Data.Any() ? "Todos retrieved" : "Todos not found",
                StatusCode = pagedResponse.Data.Any() ? StatusCodes.Status200OK : StatusCodes.Status404NotFound
            };

            return serviceResult;
        }

        public async Task<ServiceResult<TodoResponse>> CreateTodoAsync(CreateTodoRequest createTodoRequest)
        {
            ServiceResult<TodoResponse> serviceResult;

            try
            {
                var user = await _userService.GetUser();

                if(user == null)
                {
                    serviceResult = new ServiceResult<TodoResponse>()
                    {
                        Data = null,
                        Success = false,
                        Message = "Unable to retrieve authenticated user from context",
                        StatusCode = StatusCodes.Status401Unauthorized
                    };

                    return serviceResult;
                }

                var todo = _mapper.Map<Todo>(createTodoRequest);
                todo.UserId = user.Id;
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

        public async Task<ServiceResult<bool?>> UpdateTodoAsync(UpdateTodoRequest updateTodoRequest)
        {
            var serviceResult = new ServiceResult<bool?>();

            try
            {
                var todo = await _todoRepository.GetAsync(updateTodoRequest.Id);

                if (todo == null)
                {
                    serviceResult.Data = null;
                    serviceResult.Success = false;
                    serviceResult.Message = "Todo not found";
                    serviceResult.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    todo = _mapper.Map(updateTodoRequest, todo);
                    await _todoRepository.UpdateAsync(todo);
                    int saveResult = await _todoRepository.SaveAsync();

                    serviceResult.Data = null;
                    serviceResult.Success = saveResult > 0;
                    serviceResult.Message = saveResult > 0 ? "Todo updated successfully" : "Unexpected value when saving";
                    serviceResult.StatusCode = saveResult > 0 ? StatusCodes.Status204NoContent : StatusCodes.Status500InternalServerError;
                }
            }
            catch (DbUpdateException ex)
            {
                serviceResult.Data = null;
                serviceResult.Success = false;
                serviceResult.Message = $"Database error: {ex.Message}";
                serviceResult.StatusCode = StatusCodes.Status500InternalServerError;
            }
            catch (Exception ex)
            {

                serviceResult.Data = null;
                serviceResult.Success = false;
                serviceResult.Message = $"Unexpected error: {ex.Message}";
                serviceResult.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool?>> DeleteTodoAsync(int id)
        {
            ServiceResult<bool?> serviceResult = new ServiceResult<bool?>();

            try
            {
                if (!await _todoRepository.ExistsAsync(id))
                {
                    serviceResult.Data = null;
                    serviceResult.Success = false;
                    serviceResult.Message = "Todo not found";
                    serviceResult.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    await _todoRepository.DeleteAsync(id);
                    int saveResult = await _todoRepository.SaveAsync();

                    serviceResult.Data = null;
                    serviceResult.Success = saveResult > 0;
                    serviceResult.Message = saveResult > 0 ? "Todo removed successfully" : "Unexpected value when saving";
                    serviceResult.StatusCode = saveResult > 0 ? StatusCodes.Status204NoContent : StatusCodes.Status500InternalServerError;
                }
            }
            catch (DbUpdateException ex)
            {
                serviceResult.Data = null;
                serviceResult.Success = false;
                serviceResult.Message = $"Database error: {ex.Message}";
                serviceResult.StatusCode = StatusCodes.Status500InternalServerError;

            }
            catch (Exception ex)
            {
                serviceResult.Data = null;
                serviceResult.Success = false;
                serviceResult.Message = $"Unexpected error: {ex.Message}";
                serviceResult.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return serviceResult;
        }
    }
}
