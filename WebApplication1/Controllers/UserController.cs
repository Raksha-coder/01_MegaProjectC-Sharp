using Application.Responses;
using Application.userCQRS.command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> register([FromForm] postRegistration register,CancellationToken cancel)
        {
            var value = await Mediator.Send(register,cancel);

            if(value.Status == 200)
            {
                return Ok(value);
            }
            else
            {
                return BadRequest(value);
            }
        }








        [HttpPost("Login")]
        [AllowAnonymous]  //why we use this?

        public async Task<IActionResult> PostLogin([FromBody] postLogin login)
        {
            var token = await Mediator.Send(login);  //get the token
            try
            {
                if (token == null)
                {
                    var response = new LoginResponse()
                    {
                        Status = 400,
                        StatusDescription = "username and password are incorrect",
                        Role = null,
                        Error = null,
                        Token = null
                    };
                    return BadRequest(response);
                }
                else
                {
                    var response = new LoginResponse()
                    {
                        Status = 200,
                        StatusDescription = "successful",
                        Role = token.Item2,                    // role from registration
                        Error = null,
                        Token = token.Item1
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response1 = new LoginResponse()
                {
                    Status = 400,
                    StatusDescription = "error",
                    Role = null,
                    Error = ex.Message,
                    Token = null
                };
                return BadRequest(response1);
            }
        }



    }
}
