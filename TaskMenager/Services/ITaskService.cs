using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMenager.Data.Entities;

namespace TaskMenager.Services
{
    public interface ITaskService
    {
        Task AddTaskAsync(TaskItem task);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
    }
}
