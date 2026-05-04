using Microsoft.AspNetCore.Mvc;
using TaskManager_backend.Data;
using TaskManager_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManager_backend.Controllers;
[ApiController]
[Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem updated)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = updated.Title;
            task.IsCompleted = updated.IsCompleted;

            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

