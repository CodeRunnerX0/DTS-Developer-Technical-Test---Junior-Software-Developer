using Microsoft.EntityFrameworkCore;
using TaskApi.Models;

namespace TaskApi.Context 
{
    // This class represents the session with the database.
    // Inherits from DbContext so Entity Framework Core can manage entities and perform CRUD operations
    public class TaskContext : DbContext 
    {
        // Constructor that passes configuration options (connection string) to the base DbContext class
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }
        
        // Represents the Tasks table in the database
        // Each TaskItem object corresponds to a row in this table
        public DbSet<TaskItem> Tasks { get; set; } = null!;
    }
}