using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using jwt.redis.netcoreapi.Data;
using jwt.redis.netcoreapi.Models.Request;
using jwt.redis.netcoreapi.Models.Response;
using jwt.redis.netcoreapi.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jwt.redis.netcoreapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BaseResponse<LoginResponseModel>))]
        [HttpPost("Login")]
        public IActionResult Login(LoginRequestModel loginRequestModel)
        {
            var result = _loginService.Login(loginRequestModel);

            if (result.HasError)
                return BadRequest(result);

            return Ok(result);
        }


    }
}
