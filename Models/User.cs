using System.ComponentModel.DataAnnotations;

namespace Task2.Models
{

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();
    }




}
