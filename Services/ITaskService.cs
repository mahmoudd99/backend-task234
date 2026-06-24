
using Task2.Models;
namespace Task2.Services
{
    
    

    public interface ITaskService
    {
        Task<IEnumerable<TodoTask>> GetAllAsync();
        Task<TodoTask?> GetByIdAsync(int id);
        Task<TodoTask> CreateAsync(TodoTask task);
        Task UpdateAsync(TodoTask task);
        Task DeleteAsync(int id);
        Task<IEnumerable<TodoTask>> GetTasksByUserIdAsync(int userId);
    }
}
