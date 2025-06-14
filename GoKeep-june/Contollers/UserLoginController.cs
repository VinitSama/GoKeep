using GoKeep.Business;
using GoKeep.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoKeep_june.Contollers
{
    [Route("GoKeep")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserBL _userBL;

        public UserLoginController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser(RegisterRequestModel userModel)
        {
            try
            {
                var result = await _userBL.RegisterUser(userModel);
                var response = new GenericResponseModel();
                if (result == false)
                {
                    response.ResponseCode = 404;
                    response.Description = "Some Error Occur!";
                }
                else
                {
                    response.ResponseCode = 201;
                    response.Description = "User Is Registered";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequestModel userModel)
        {
            try
            {
                var result = await _userBL.LoginUser(userModel);
                var response = new GenericResponseModel<TokenResponseModel>(result);
                if (result == null)
                {
                    response.ResponseCode = 401;
                    response.Description = "Email or Password does not match!";
                    return Unauthorized(response);
                }
                else
                {
                    response.ResponseCode = 200;
                    response.Description = "User Can Login";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet("{oldToken}")]
        public async Task<IActionResult> RefreshToken(string oldToken)
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(new GenericResponseModel { ResponseCode = 400, Description = "Email not found in claims." });
                }
                var refreshModel = new RefreshTokenModel()
                {
                    Email = email,
                    OldToken = oldToken
                };
                var result = await _userBL.RefreshToken(refreshModel);
                var response = new GenericResponseModel<TokenResponseModel>(result);
                if (result == null)
                {
                    response.ResponseCode = 401;
                    response.Description = "Please Authorize Again";
                    return BadRequest(response);
                }
                else
                {
                    response.ResponseCode = 200;
                    response.Description = "Refreshed Token";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
