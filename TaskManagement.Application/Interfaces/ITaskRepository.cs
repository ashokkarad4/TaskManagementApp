using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId);
        Task<IEnumerable<TaskItem>> GetByUserIdAndStatusAsync(int userId, bool isCompleted);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
        Task SaveChangesAsync();
    }
}
