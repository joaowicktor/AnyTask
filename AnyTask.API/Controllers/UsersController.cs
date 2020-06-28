using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Interfaces;
using AnyTask.API.Data.Repositories;
using AnyTask.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyTask.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public UsersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Retrieve all users.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<User>> ListUsers()
        {
            return Ok(_uow.UserRepository.FindAll());
        }

        /// <summary>
        /// Register user.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                if (await _uow.UserRepository.FindByEmailAsync(user.Email) != null)
                    return BadRequest(new ErrorMessage("Email already registered"));

                user.Password = user.Password.SHA256Encrypt();
                _uow.UserRepository.Create(user);
                var rows = await _uow.CommitAsync();

                if (rows == 0)
                    return BadRequest(new ErrorMessage("Something went wrong when create user"));

                return Ok(new { message = "User create successfully!" });
            }
            catch (Exception e)
            {
                _uow.Rollback();
                return BadRequest(new ErrorMessage(e.Message));
            }
        }
    }
}
