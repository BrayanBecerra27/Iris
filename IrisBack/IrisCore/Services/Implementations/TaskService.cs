using IrisCore.DTOs;
using IrisCore.Entities;
using IrisCore.Exceptions;
using IrisCore.InterfacesRepositories;
using IrisCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisCore.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _iTaskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _iTaskRepository = taskRepository;
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
        public async Task<string> AddTask(TaskRequestDTO task)
        {
            var taskEntitie = new TaskToDo
            {
                Id = GenerateId(),
                Description = task.Description,
                IsCompleted = false,
                IsFavorite = false,
                CreationDate = DateTime.Now.AddHours(-5)
            };
            await _iTaskRepository.AddAsync(taskEntitie);
            return taskEntitie.Id;
        }

        public async Task<bool> DeleteTask(string idTask)
        {
            var task = await _iTaskRepository.FindAsync(idTask) ??
                 throw new BusinessException($"Task: {idTask} was not found");
            await _iTaskRepository.RemoveAsync(task);
            return await _iTaskRepository.FindAsync(idTask) == null;
        }

        public async Task<IEnumerable<TaskDTO>> GetAllTask()
        {
            var taskList = await _iTaskRepository.GetAllAsync();
            return taskList.Select(s => new TaskDTO
            {
                Id = s.Id,
                Description = s.Description,
                IsCompleted = s.IsCompleted,
                IsFavorite = s.IsFavorite
            });
        }

        public async Task<bool> MarkTaskCompleted(string idTask, bool completed)
        {
            var task = await _iTaskRepository.FindAsync(idTask) ??
                 throw new BusinessException($"Task: {idTask} was not found");
            task.IsCompleted = completed;
            await _iTaskRepository.UpdateAsync(task);
            return !string.IsNullOrEmpty(task.Id);
        }

        public async Task<bool> MarkTaskFavourite(string idTask, bool favourite)
        {
            var task = await _iTaskRepository.FindAsync(idTask) ??
                 throw new BusinessException($"Task: {idTask} was not found");
            task.IsFavorite = favourite;
            await _iTaskRepository.UpdateAsync(task);
            return !string.IsNullOrEmpty(task.Id);
        }

        public async Task<bool> UpdateTask(TaskRequestDTO task)
        {
            var taskData = await _iTaskRepository.FindAsync(task.Id) ??
                 throw new BusinessException($"Task: {task.Id} was not found");
            taskData.IsFavorite = task.IsFavorite;
            taskData.IsCompleted = task.IsCompleted;
            taskData.Description = task.Description;
            await _iTaskRepository.UpdateAsync(taskData);
            return !string.IsNullOrEmpty(task.Id);
        }

       
    }
}
