using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.DBContext;

namespace TaskManagement.Infrastructure.Service
{
    public class TaskService : ITaskService
    {
        private readonly TaskManagementDbContext _context;

        public TaskService(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetTasks(int userId, bool? isCompleted)
        {
            var query = _context.TaskItems.Where(x => x.UserId == userId);

            if (isCompleted.HasValue)
                query = query.Where(x => x.IsCompleted == isCompleted);

            return await query.ToListAsync();
        }

        public async Task<TaskItem> CreateTask(TaskDto dto, int userId)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task UpdateTask(int id, TaskDto dto)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) throw new Exception("Task not found");

            task.Title = dto.Title;
            task.Description = dto.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) throw new Exception("Task not found");

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleStatus(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) throw new Exception("Task not found");

            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();
        }
    }
}
