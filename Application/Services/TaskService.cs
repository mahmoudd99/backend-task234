using Task2.Application.Interfaces;
using Task2.Domain;

namespace Task2.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TodoTask>> GetAllTasks()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TodoTask?> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<TodoTask> CreateAsync(TodoTask task)
        {
            return await _taskRepository.CreateAsync(task);
        }

        public async Task UpdateAsync(TodoTask task)
        {
            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TodoTask>> GetTasksByUserIdAsync(int userId)
        {
            return await _taskRepository.GetTasksByUserIdAsync(userId);
        }

        public async Task<IEnumerable<TodoTask>> GetAllAsync()
        {
            return await _taskRepository.GetAllAsync();
        }
    }
}