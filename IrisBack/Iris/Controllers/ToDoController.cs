using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using IrisCore.DTOs;
using IrisCore.Services.Interfaces;
using Iris.ViewModels.Response;

namespace Iris.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ITaskService _iTaskService;
        private readonly IValidator<TaskRequestDTO> _validator;

        public ToDoController(ITaskService service, IValidator<TaskRequestDTO> validator)
        {
            _iTaskService = service;
            _validator = validator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(new ResultResponse<IEnumerable<TaskDTO>> { Data = await _iTaskService.GetAllTask() });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTask([FromBody] TaskRequestDTO task)
        {
            await _validator.ValidateAndThrowAsync(task);
            return Ok(new ResultResponse<string> { Data = await _iTaskService.AddTask(task) });
        }

        [HttpDelete("{idTask}")]
        [Authorize]
        public async Task<IActionResult> DeleteTask(string idTask)
        {
            return Ok(new ResultResponse<bool> { Data = await _iTaskService.DeleteTask(idTask) });
        }

        [HttpPut("{idTask}/favourite")]
        [Authorize]
        public async Task<IActionResult> MarkTaskFavourite(string idTask, bool favourite)
        {
            return Ok(new ResultResponse<bool> { Data = await _iTaskService.MarkTaskFavourite(idTask, favourite)});
        }

        [HttpPut("{idTask}/done")]
        [Authorize]
        public async Task<IActionResult> MarkTaskCompleted(string idTask,bool completed)
        {
            return Ok(new ResultResponse<bool> { Data = await _iTaskService.MarkTaskCompleted(idTask, completed)});
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateTask([FromBody] TaskRequestDTO task)
        {
            await _validator.ValidateAndThrowAsync(task);
            return Ok(new ResultResponse<bool> { Data = await _iTaskService.UpdateTask(task)});
        }
    }
}
