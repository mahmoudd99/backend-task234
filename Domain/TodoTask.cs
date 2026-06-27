using System.ComponentModel.DataAnnotations;
using Task2.Models;

namespace Task2.Domain
{

    public class TodoTask
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        internal async Task CreateAsync(TodoTask task)
        {
            throw new NotImplementedException();
        }
    }






}
