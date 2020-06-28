using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Interfaces;
using AnyTask.API.Data.Repositories;
using AnyTask.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var lstUsers = _uow.UserRepository.FindAll()
                .Select(u => new User(u.Id, u.Name, u.Email, null));

            return Ok(lstUsers);
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
                var userExists = await _uow.UserRepository.FindByCondition(u => u.Email == user.Email)
                    .SingleOrDefaultAsync();

                if (userExists != null)
                    return BadRequest(new Response(false, "Email already registered"));

                user.Password = user.Password.SHA256Encrypt();
                _uow.UserRepository.Create(user);
                var rows = await _uow.CommitAsync();

                if (rows == 0)
                    return BadRequest(new Response(false, "Something went wrong when create user"));

                return Ok(new Response(true, "User create successfully!"));
            }
            catch (Exception e)
            {
                _uow.Rollback();
                return BadRequest(new ErrorMessage(e.Message));
            }
        }
    }
}
