using Task2.Domain;

namespace Task2.Application.Interfaces
{    public interface ITaskService
    {
        Task<IEnumerable<TodoTask>> GetAllAsync();
        Task<TodoTask?> GetByIdAsync(int id);
        Task<TodoTask> CreateAsync(TodoTask task);
        Task UpdateAsync(TodoTask task);
        Task DeleteAsync(int id);
        Task<IEnumerable<TodoTask>> GetTasksByUserIdAsync(int userId);
    }
}
