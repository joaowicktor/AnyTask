using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Interfaces;
using AnyTask.API.Helpers;
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
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public AuthController(IUnitOfWork uow, IConfiguration config)
        {
            _uow = uow;
            _config = config;
        }

        /// <summary>
        /// Login user.
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserLogin login)
        {
            var user = await _uow.UserRepository.FindByCondition(u => u.Email == login.Email).SingleOrDefaultAsync();

            if (user == null)
                return BadRequest(new Response(false, "User or password invalid"));

            if (login.Password.SHA256Encrypt() != user.Password)
                return BadRequest(new Response(false, "User or password invalid"));

            user.Password = string.Empty;

            var jwt = new JwtAuth(_config.GetValue<string>("JwtSettings:Secret"));
            var token = jwt.GenerateToken(user);

            return new { user, token };
        }
    }
}
