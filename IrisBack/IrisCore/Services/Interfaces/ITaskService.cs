using IrisCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisCore.Services.Interfaces
{
    public interface ITaskService
    {
        Task<string> AddTask(TaskRequestDTO task);
        Task<bool> DeleteTask(string idTask);
        Task<IEnumerable<TaskDTO>> GetAllTask();
        Task<bool> MarkTaskCompleted(string idTask, bool completed);
        Task<bool> MarkTaskFavourite(string idTask, bool favourite);
        Task<bool> UpdateTask(TaskRequestDTO task);
    }
}
