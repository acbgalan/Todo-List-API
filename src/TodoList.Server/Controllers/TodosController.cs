using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Entities;
using TodoList.Data.Repositories;

namespace TodoList.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet("{id:int}", Name = "GetTodo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _todoRepository.GetAsync(id);

            if (todo == null)
            {
                return NotFound("Todo not found");
            }

            return Ok(todo);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Todo>>> GetsTodo()
        {
            var todoList = await _todoRepository.GetAllAsync();

            if (!todoList.Any())
            {
                return NotFound("Todos not found");
            }

            return Ok(todoList);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateTodo(Todo todo)
        {
            if (todo == null)
            {
                return BadRequest();
            }

            await _todoRepository.AddAsync(todo);
            int saveResult = await _todoRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "");
            }

            return CreatedAtRoute("GetTodo", new { id = todo.Id }, todo);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTodo(int id, Todo todo)
        {
            if (todo == null)
            {
                return BadRequest();
            }

            if (id != todo.Id)
            {
                return BadRequest("Id mismatch");
            }

            var todoDb = await _todoRepository.GetAsync(id);

            if (todoDb == null)
            {
                return NotFound("Todo not found");
            }

            todoDb.Title = todo.Title;
            todoDb.Description = todo.Description;

            await _todoRepository.UpdateAsync(todoDb);
            int saveResult = await _todoRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("{int:int}")]
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}
