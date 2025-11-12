using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Entities;
using TodoList.Data.Repositories;
using TodoList.Server.Services.TodoService;
using TodoList.Shared;
using TodoList.Shared.Todo;

namespace TodoList.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITodoService _todoService;

        public TodosController(IMapper mapper, ITodoService todoService)
        {
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
        public async Task<ActionResult<PagedResponse<TodoResponse>>> GetsTodo([FromQuery] QueryParameters queryParametersTodo)
        {
            var serviceResponse = await _todoService.GetTodosAsync(queryParametersTodo);

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

            var serviceResponse = await _todoService.UpdateTodoAsync(updateTodoRequest);

            if (!serviceResponse.Success)
            {
                return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var serviceResponse = await _todoService.DeleteTodoAsync(id);

            if (!serviceResponse.Success)
            {
                return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
