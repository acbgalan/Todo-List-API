using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Entities;
using TodoList.Data.Repositories;
using TodoList.Server.Services.TodoService;
using TodoList.Shared.Todo;

namespace TodoList.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly ITodoService _todoService;

        public TodosController(ITodoRepository todoRepository, IMapper mapper, ITodoService todoService)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _todoService = todoService;
        }

        [HttpGet("{id:int}", Name = "GetTodo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoResponse>> GetTodo(int id)
        {
            var serviceResponse = await _todoService.GetTodoAsync(id);

            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TodoResponse>>> GetsTodo()
        {
            var serviceResponse = await _todoService.GetTodosAsync();

            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateTodo(CreateTodoRequest createTodoRequest)
        {
            if (createTodoRequest == null)
            {
                return BadRequest();
            }

            var serviceResponse = await _todoService.CreateTodoAsync(createTodoRequest);

            if (!serviceResponse.Success)
            {
                return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
            }

            return CreatedAtRoute("GetTodo", new { id = serviceResponse.Data!.Id }, serviceResponse.Data);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTodo(int id, UpdateTodoRequest updateTodoRequest)
        {
            if (updateTodoRequest == null)
            {
                return BadRequest();
            }

            if (id != updateTodoRequest.Id)
            {
                return BadRequest("Id mismatch");
            }

            var todo = await _todoRepository.GetAsync(id);

            if (todo == null)
            {
                return NotFound("Todo not found");
            }

            todo = _mapper.Map(updateTodoRequest, todo);
            await _todoRepository.UpdateAsync(todo);
            int saveResult = await _todoRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected value when saving");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            if (!await _todoRepository.ExistsAsync(id))
            {
                return NotFound("Todo not found");
            }

            await _todoRepository.DeleteAsync(id);
            int saveResult = await _todoRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected value when saving");
            }

            return NoContent();
        }
    }
}
