using Moq; // Library used to create mock (fake) objects.
using Xunit; // The testing framework used to define and run the tests
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Controller;
using TaskApi.Models;
using TaskApi.Context;


namespace TaskApi.Tests;

// This class contains all the unit tests for the TaskItemController
    public class TaskUnitTest
{
    private readonly TaskContext _context;
    private readonly TaskItemController _controller;
    
    // Mock object for the ILogger dependency so there is no real logging during tests
    private readonly Mock<ILogger<TaskItemController>> _mockLogger;
    
    public TaskUnitTest()
    {
        // Configure EF to use an in memory database for testing
        var contextOptions = new DbContextOptionsBuilder<TaskContext>()
                                          .UseInMemoryDatabase(databaseName: "TestDb")
                                          .Options;
        // Mock logger
       _mockLogger = new Mock<ILogger<TaskItemController>>();

       // Create in memory database context
       _context = new TaskContext(contextOptions);

       // Create the controller using the test context and mock logger
       _controller = new TaskItemController(_context, _mockLogger.Object);
    }

    
    [Fact] // Marks this method as a test that Xunit should run
    public async Task PostTaskItem()
    {
        // Mock TaskItem object 
        var mockTask = new TaskItem
        {
            Title = "Test Task",
            Description = "This is a test task",
            Status  = true,
            DueDate = DateTime.UtcNow.AddDays(1)

        };

        // Call to controller method to add the TaskItem object
        var result = await _controller.CreateTaskItem(mockTask);

        //Verify the result is of type ActionResult<TaskItem>
        var actionResult = Assert.IsType<ActionResult<TaskItem>>(result);

        // Check if return type is created at action and returns 201
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        
        // Verify that the correct action name was used to build the URI
        Assert.Equal("CreateTaskItem", createdAtActionResult.ActionName);

        // Verify that the data returned in the body of the response is a TaskItem object
        Assert.IsType<TaskItem>(createdAtActionResult.Value);

    }
}



