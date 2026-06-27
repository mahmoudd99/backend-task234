using Microsoft.EntityFrameworkCore;
using Task2.Application.Interfaces;
using Task2.Domain;
using Task2.Infrastructure.Data;

namespace Task2.Infrastructure.Repositories
{


    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoTask>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TodoTask?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddAsync(TodoTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoTask> CreateAsync(TodoTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateAsync(TodoTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                throw new Exception("Task not found");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoTask>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }




}
