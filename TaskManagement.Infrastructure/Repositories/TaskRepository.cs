using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.DBContext;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _context;

        public TaskRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId)
        {
            return await _context.TaskItems
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByUserIdAndStatusAsync(int userId, bool isCompleted)
        {
            return await _context.TaskItems
                .Where(t => t.UserId == userId && t.IsCompleted == isCompleted)
                .OrderByDescending(t => t.Created)
                .ToListAsync();
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TaskItem task)
        {
            _context.TaskItems.Remove(task);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
