using AnyTask.API.Data.Interfaces;
using AnyTask.API.Helpers;
using AnyTask.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AnyTask.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public TasksController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get task by id.
        /// </summary>
        [HttpGet("{taskId:int}")]
        public async Task<IActionResult> GetTaskById([FromRoute] int taskId)
        {
            return Ok(await _uow.TaskRepository.FindById(taskId));
        }

        /// <summary>
        /// List tasks by user id.
        /// </summary>
        [HttpGet]
        public IActionResult ListTasksByUserId([FromQuery] int userId)
        {
            return Ok(_uow.TaskRepository.FindByCondition(t => t.UserId == userId));
        }

        /// <summary>
        /// Create new task.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskViewModel task)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new Response(false, "Data is invalid"));

                var user = await _uow.UserRepository.FindById(task.UserId);
                if (user == null)
                    return BadRequest(new Response(false, "User not found"));

                var newTask = new Data.Entities.Task(task.Description, task.UserId);
                _uow.TaskRepository.Create(newTask);
                var rows = await _uow.CommitAsync();

                if (rows == 0)
                    return BadRequest(new Response(false, "Something went wrong when create task"));

                return Ok(new Response(true, "Task create successfully!", newTask));
            } catch (Exception e)
            {
                _uow.Rollback();
                return BadRequest(new Response(false, e.Message));
            }
        }

        /// <summary>
        /// Update task.
        /// </summary>
        [HttpPut("{taskId:int}")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskViewModel task, [FromRoute] int taskId)
        {
            try
            {
                var oldTask = await _uow.TaskRepository.FindById(taskId);
                if (oldTask == null)
                    return BadRequest(new Response(false, "Task not found"));

                oldTask.Update(task.Description);
                _uow.TaskRepository.Update(oldTask);
                var rows = await _uow.CommitAsync();

                if (rows == 0)
                    return BadRequest(new Response(false, "Something went wrong when update task"));

                return Ok(new Response(true, "Task update successfully!"));
            }
            catch (Exception e)
            {
                _uow.Rollback();
                return BadRequest(new Response(false, e.Message));
            }
        }

        /// <summary>
        /// Set task as concluded.
        /// </summary>
        [HttpPatch("{taskId:int}")]
        public async Task<IActionResult> SetTaskAsConcluded([FromRoute] int taskId)
        {
            try
            {
                var task = await _uow.TaskRepository.FindById(taskId);
                if (task == null)
                    return BadRequest(new Response(false, "Task not found"));

                _uow.TaskRepository.SetAsConcluded(taskId);
                var rows = await _uow.CommitAsync();

                if (rows == 0)
                    return BadRequest(new Response(false, "Something went wrong when set task as concluded"));

                return Ok(new Response(true, "Task concluded!"));
            }
            catch (Exception e)
            {
                _uow.Rollback();
                return BadRequest(new Response(false, e.Message));
            }
        }

        /// <summary>
        /// Delete task.
        /// </summary>
        [HttpDelete("{taskId:int}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int taskId)
        {
            try
            {
                var task = await _uow.TaskRepository.FindById(taskId);
                if (task == null)
                    return BadRequest(new Response(false, "Task not found"));

                _uow.TaskRepository.Delete(task);
                var rows = await _uow.CommitAsync();

                if (rows == 0)
                    return BadRequest(new Response(false, "Something went wrong when delete task"));

                return Ok(new Response(true, "Task delete successfully!"));
            } catch (Exception e)
            {
                _uow.Rollback();
                return BadRequest(new Response(false, e.Message));
            }
        }
    }
}
