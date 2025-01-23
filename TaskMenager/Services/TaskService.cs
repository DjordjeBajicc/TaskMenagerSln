using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMenager.Data.Entities;

namespace TaskMenager.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskContext _context;

        public TaskService(TaskContext _context)
        {
            this._context = _context;
        }
        public async Task AddTaskAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask != null)
            {
                _context.Tasks.Remove(existingTask);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public Task<TaskItem> GetTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);

            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                existingTask.Description = task.Description;
                existingTask.Deadline = task.Deadline;

                await _context.SaveChangesAsync();
            }
        }
    }
}
