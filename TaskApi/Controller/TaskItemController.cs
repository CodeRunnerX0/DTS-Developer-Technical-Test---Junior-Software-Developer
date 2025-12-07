using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Models;
using TaskApi.Context;

namespace TaskApi.Controller
{
    // Defines base route for all actions in this controller (api/TaskItem)
    [Route("api/[controller]")]
    [ApiController]

    public class TaskItemController : ControllerBase
    {
        private readonly TaskContext _context;
        private readonly ILogger<TaskItemController> _logger;

        // Constructor where dependencies (TaskContext and ILogger) are injected here
        public TaskItemController(TaskContext context, ILogger<TaskItemController> logger) // Injecting the dependicies in the contructor
        {
            _context = context;
            _logger = logger;
        }

    // GET: api/TaskItem
    // Handles requests to retrieve all tasks.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
    {
        try
        {
            _logger.LogInformation("Fetching all task items."); // Logging the start of the operation
            var tasks = await _context.Tasks.ToListAsync(); // Query the db fo all tasks

            // If no tasks exist it logs a warning and returns a not found message
            if (tasks == null || !tasks.Any()) 
            {
                _logger.LogWarning("No task items found.");
                return NotFound("No task items found.");
            }
            return Ok(tasks); // Returns a list of tasks with a 200 status code
        }
        catch (Exception ex) // Error handling logging a error if something goes wrong and returns status code 500
        {
            _logger.LogError(ex, "An error occurred while fetching task items.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    // POST: api/TaskItem
    // Handles requests to create a new task.
    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTaskItem(TaskItem taskItem)
    {
        try 
        {
            // Validation to check for incoming task is not null
            if (taskItem == null)
            {
                // Log a warning and return a bad request response
                _logger.LogWarning("Task item is null.");
                return BadRequest("Task item is null.");
            }

            // Adds a new task to the local context
            _context.Tasks.Add(taskItem); 

            // Save the task to the database 
            await _context.SaveChangesAsync(); 

            _logger.LogInformation($"Task with ID {taskItem.TaskId} was created."); 

            // Returns a 201 created response, including the URI to access it
            return CreatedAtAction(nameof(CreateTaskItem), new { id = taskItem.TaskId }, taskItem);
            
        }
        catch (Exception ex)
        {
            // Log the error and return a 500 internal server error response
            _logger.LogError(ex, "An error occurred while creating the task item.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
        
    }
   
}
}