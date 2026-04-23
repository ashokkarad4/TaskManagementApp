using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> CreateTask(TaskDto dto, int userId)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                UserId = userId,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return task;
        }

        public async Task DeleteTask(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            await _taskRepository.DeleteAsync(task);
            await _taskRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasks(int userId, bool? isCompleted)
        {
            if (isCompleted.HasValue)
            {
                return await _taskRepository.GetByUserIdAndStatusAsync(userId, isCompleted.Value);
            }

            return await _taskRepository.GetByUserIdAsync(userId);
        }

        public async Task ToggleStatus(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            task.IsCompleted = !task.IsCompleted;
            task.Updated = DateTime.Now;

            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();
        }

        public async Task UpdateTask(int id, TaskDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Updated = DateTime.Now;

            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();
        }
    }
}
