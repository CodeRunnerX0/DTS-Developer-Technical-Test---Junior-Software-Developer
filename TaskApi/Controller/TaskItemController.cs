using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Models;
using TaskApi.Context;

namespace TaskApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskItemController : ControllerBase
    {
        private readonly TaskContext _context;
        private readonly ILogger<TaskItemController> _logger;

        public TaskItemController(TaskContext context, ILogger<TaskItemController> logger) // Injecting the dependicies in the contructor
        {
            _context = context;
            _logger = logger;
        }

//Get all tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
    {
        try
        {
            _logger.LogInformation("Fetching all task items."); //Logging the start of the operation
            var tasks = await _context.Tasks.ToListAsync(); // Retrieving all tasks from the database

            if (tasks == null || !tasks.Any()) // If no tasks exist it logs a warning and returns not found message
            {
                _logger.LogWarning("No task items found.");
                return NotFound("No task items found.");
            }
            return Ok(tasks); // Else returns a list of tasks with a 200 status code
        }
        catch (Exception ex) // Error handling logging a error if something goes wrong and returns status code 500
        {
            _logger.LogError(ex, "An error occurred while fetching task items.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    //Post a new task
    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTaskItem(TaskItem taskItem)
    {
        try 
        {
            if (taskItem == null)
            {
                _logger.LogWarning("Task item is null.");
                return BadRequest("Task item is null.");
            }

            _context.Tasks.Add(taskItem); 
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Task with ID {taskItem.TaskId} was created."); 
            return CreatedAtAction(nameof(CreateTaskItem), new { id = taskItem.TaskId }, taskItem);
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the task item.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
        
    }
   
}
}