using DataProvider.IProvider;
using DataProvider.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.DTOs.Request;
using Models.DTOs.Response.BaseResponse;
using Models.Models;
using System.Net;
using static DataAccess.ApplicationContext.ApplicationDBContext;

namespace GalaxyBackEndTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthProvider _authProvider;

        public AuthController(ApplicationDbContext context, IAuthProvider authProvider)
        {
            _context = context;
            _authProvider = authProvider;  
        }

        [HttpPost("Register")]
        public async Task<BaseAPIResponse> Register(RegisterRequest registerRequest)
        {
            BaseAPIResponse response = BaseAPIResponse.Create(HttpStatusCode.OK, null, "");
            string errorDateEntered = "Please make sure you have entered all data";

            if (registerRequest == null)
            {
                response = BaseAPIResponse.Create(HttpStatusCode.BadRequest, null, "data not found");
                return response;
            }

            if (!_authProvider.AuthenticationRepo.CheckRequestedObj(registerRequest))
            {
                response = BaseAPIResponse.Create(HttpStatusCode.BadRequest, null, "The data is incorrect");
                return response;
            }

            if (await _authProvider.AuthenticationRepo.checkIfEmailOrPhoneExists(registerRequest.Email, registerRequest.Mopile))
            {
                response = BaseAPIResponse.Create(HttpStatusCode.BadRequest, null, "Email or phone number has been used before");
                return response;
            }

            var validation = _authProvider.AuthenticationRepo.CheckPasswordStrength(registerRequest.Password);
            if (!string.IsNullOrEmpty(validation))
            {
                response = BaseAPIResponse.Create(HttpStatusCode.BadRequest, null, validation);
                return response;
            }

            var registered = await _authProvider.AuthenticationRepo.UserRegister(registerRequest);
            if (registered)
            {
                response = BaseAPIResponse.Create(HttpStatusCode.OK, null, "successfully registered");
            }
            else
            {
                response = BaseAPIResponse.Create(HttpStatusCode.InternalServerError, null, "An error occurred during registration");
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();

            var result = await _authProvider.AuthenticationRepo.Login(loginRequest);
            if (result == null)
            {
                return NotFound(new { Message = "user not found" });
            }

            return Ok(result);
        }

    }
}





