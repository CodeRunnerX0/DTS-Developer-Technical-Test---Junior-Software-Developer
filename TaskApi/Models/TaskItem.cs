using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId {get; set;}

        [Required]
        public string Title {get; set;} 

        public string? Description {get; set;} 

        [Required]
        public bool Status {get; set;}
        
        [Required]
        public DateTime DueDate {get; set;}
    }
}

