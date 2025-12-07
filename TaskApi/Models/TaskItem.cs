using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models
{
    // This class defines the structure of the task item object in the database
    public class TaskItem
    {
        // Primary key
        [Key] 
        public int TaskId {get; set;}

        // Title of the task 
        [Required]
        public string Title {get; set;} 
        
        // Description is a oprional field as some taks are straight forward
        public string? Description {get; set;} 

        // Status of the task 
        [Required]
        public bool Status {get; set;}
        
        // Due date of the task
        [Required]
        public DateTime DueDate {get; set;}
    }
}

