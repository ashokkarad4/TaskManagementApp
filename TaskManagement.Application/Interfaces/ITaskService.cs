using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetTasks(int userId, bool? isCompleted);
        Task<TaskItem> CreateTask(TaskDto dto, int userId);
        Task UpdateTask(int id, TaskDto dto);
        Task DeleteTask(int id);
        Task ToggleStatus(int id);
    }
}
