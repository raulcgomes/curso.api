using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Configurations;
using curso.api.Filters;
using curso.api.Infrastructure.Data;
using curso.api.Models;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace curso.api.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserController(
            IUserRepository userRepository, 
            IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// This service allows to auth an user previously registered
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Success on the auth", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Mandatory fields needs attention", Type = typeof(ValidateFieldViewModeIOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal server error", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("login")]
        [ValidateModelStateCustom]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            var user = _userRepository.GetUser(loginViewModelInput.Login);

            if (user == null)
            {
                return BadRequest("There was an error when try to access");
            }

            var userViewModelOutput = new UserViewModelOutput()
            {
                Id = user.UserId,
                Login = loginViewModelInput.Login,
                Email = user.Email
            };

            var token = _authenticationService.GenerateToken(userViewModelOutput);


            return Ok(new
            {
                Token = token,
                User = userViewModelOutput,
            });
        }

        /// <summary>
        /// This service allows to register unregistered users
        /// </summary>
        /// <param name="loginViewModelInput">View model resgister login</param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Success on the auth", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Mandatory fields needs attention", Type = typeof(ValidateFieldViewModeIOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal server error", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("register")]
        [ValidateModelStateCustom]
        public IActionResult Register(RegisterViewModelInput loginViewModelInput)
        {
            /*var optionsBilder = new DbContextOptionsBuilder<CourseDbContext>();
            optionsBilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=DOTNETCOURSE;Trusted_Connection=True;");
            CourseDbContext context = new CourseDbContext(optionsBilder.Options);*/

            /*var pendingMigrations = context.Database.GetPendingMigrations();

            if (pendingMigrations.Count() > 0)
            {
                context.Database.Migrate();
            }*/

            var user = new User();
            user.Login = loginViewModelInput.Login;
            user.Password = loginViewModelInput.Password;
            user.Email = loginViewModelInput.Email;
            _userRepository.Add(user);
            _userRepository.Commit();

            return Created("", loginViewModelInput);
        }
    }
}
