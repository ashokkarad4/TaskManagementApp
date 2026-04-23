using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

namespace TaskManagement.API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User not found in token");
            }
            return int.Parse(userIdClaim.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool? isCompleted)
        {
            var tasks = await _service.GetTasks(GetUserId(), isCompleted);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
            {
                throw new ArgumentException("Task title is required");
            }

            var task = await _service.CreateTask(dto, GetUserId());
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
            {
                throw new ArgumentException("Task title is required");
            }

            await _service.UpdateTask(id, dto);
            return Ok(new { Message = "Task updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteTask(id);
            return Ok(new { Message = "Task deleted successfully" });
        }

        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _service.ToggleStatus(id);
            return Ok(new { Message = "Task status updated successfully" });
        }
    }
}
