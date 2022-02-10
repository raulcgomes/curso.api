using curso.api.Filters;
using curso.api.Models;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace curso.api.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
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
            /*if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModeIOutput(ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage)));
            }*/

            return Ok(loginViewModelInput);
        }

        [HttpPost]
        [Route("register")]
        [ValidateModelStateCustom]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {

            return Created("", registerViewModelInput);
        }
    }
}
