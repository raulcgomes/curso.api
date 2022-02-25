using curso.api.Filters;
using curso.api.Models;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var userViewModelOutput = new UserViewModelOutput()
            {
                Code = 1,
                Login = "raul.gomes",
                Email = "raul.gomes@gmail.com"
            };



            var secret = Encoding.ASCII.GetBytes("MzfsT&d9gprP>!9$Es(X!5g@;ef!5sbk:jH\\2.}8ZP'qY#7");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userViewModelOutput.Code.ToString()),
                    new Claim(ClaimTypes.Name, userViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, userViewModelOutput.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);


            return Ok(new
            {
                Token = token,
                User = userViewModelOutput,
            });
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
