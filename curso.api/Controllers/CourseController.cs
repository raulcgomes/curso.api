using curso.api.Models;
using curso.api.Models.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        /// <summary>
        /// This service allows to create a course for the auth user.
        /// </summary>
        /// <returns>Return status 201 and data from course of user</returns>
        [SwaggerResponse(statusCode: 201, description: "Success on create course", Type = typeof(CourseViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Unauthorized")]
        [HttpPost]
        [Route("")]

        public async Task<IActionResult> Post(CouseViewModelInput courseViewModelInput)
        {
            var userCode = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", courseViewModelInput);
        }

        /// <summary>
        /// Este serviço permite obter todos os cursos ativos do usuário.
        /// </summary>
        /// <returns>Retorna status ok e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 200, description: "Successo on getting a course", Type = typeof(CourseViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Unauthorized")]
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var courses = new List<CourseViewModelOutput>();

            //var userCode = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            courses.Add(new CourseViewModelOutput()
            {
                Login = "",
                Description = "Test",
                Name = "Test"
            });

            return Ok(courses);
        }
    }
}
