using Moq;
using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Controller;
using TaskApi.Models;
using TaskApi.Context;


namespace TaskApi.Tests;

    public class TaskUnitTest
{
    private readonly TaskContext _context;
    private readonly TaskItemController _controller;
    
    private readonly Mock<ILogger<TaskItemController>> _mockLogger;
    
    public TaskUnitTest()
    {
        var contextOptions = new DbContextOptionsBuilder<TaskContext>()
                                          .UseInMemoryDatabase(databaseName: "TestDb")
                                          .Options;

       _mockLogger = new Mock<ILogger<TaskItemController>>();
       _context = new TaskContext(contextOptions);
       _controller = new TaskItemController(_context, _mockLogger.Object);
    }

    [Fact]
    public async Task PostTaskItem()
    {
        var mockTask = new TaskItem
        {
            Title = "Test Task",
            Description = "This is a test task",
            Status  = true,
            DueDate = DateTime.UtcNow.AddDays(1)

        };

        var result = await _controller.CreateTaskItem(mockTask);
        var actionResult = Assert.IsType<ActionResult<TaskItem>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        
        Assert.Equal("CreateTaskItem", createdAtActionResult.ActionName);
        Assert.IsType<TaskItem>(createdAtActionResult.Value);

    }
}



