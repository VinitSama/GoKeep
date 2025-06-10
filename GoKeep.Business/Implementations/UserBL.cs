using GoKeep.Model;
using GoKeep.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;
        private readonly IConfiguration _config;
        public UserBL(IUserRL userRL, IConfiguration configuration)
        {
            _userRL = userRL;
            _config = configuration;
        }
        public async Task<bool> RegisterUser(RegisterRequestModel userModel)
        {
            string passwordHash = PasswordService.HashPassword(userModel.Password);
            try
            {
                return await _userRL.RegisterUser(userModel, passwordHash);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TokenResponseModel> LoginUser(LoginRequestModel userModel)
        {
            //string environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //Console.WriteLine($"The current environment is: {environmentVariable}");
            //Console.WriteLine(_config["Jwt:Key"]);
            //Console.WriteLine(_config["Jwt:Issuer"]);
            //Console.WriteLine(_config["Jwt:Audience"]);
            //Console.WriteLine(_config["Jwt:ExpiryInMinutes"]);
            //Console.WriteLine(_config["ExpiryInMinutes"]);
            try
            {
                var result = await _userRL.LoginUser(userModel);
                if (result != null)
                {
                    bool isPasswordVerified = PasswordService.VerifyPassword(result.Password, userModel.Password);
                    Console.WriteLine(Environment.GetEnvironmentVariable("JWT_ISSUER"));
                    Console.WriteLine(Environment.GetEnvironmentVariable("JWT_AUDIENCE"));
                    var JwtToken = AuthService.GenerateToken(userModel.Email, Environment.GetEnvironmentVariable("JWT_KEY"), Environment.GetEnvironmentVariable("JWT_ISSUER"), Environment.GetEnvironmentVariable("JWT_AUDIENCE"), Environment.GetEnvironmentVariable("JWT_EXPIRATY_IN_MINUTE"));
                    var token = new TokenResponseModel()
                    {
                        JwtToken = JwtToken,
                        RefreshToken = result.RefreshToken
                    };
                    return token;
                }
                return null!;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TokenResponseModel> RefreshToken(RefreshTokenModel refreshModel)
        {
            try
            {
                var result = await _userRL.RefreshToken(refreshModel);
                if (result == null)
                {
                    return null!;
                }
                var JwtToken = AuthService.GenerateToken(refreshModel.Email, Environment.GetEnvironmentVariable("JWT_KEY"), Environment.GetEnvironmentVariable("JWT_ISSUER"), Environment.GetEnvironmentVariable("JWT_AUDIENCE"), Environment.GetEnvironmentVariable("JWT_EXPIRATY_IN_MINUTE"));
                var token = new TokenResponseModel()
                {
                    JwtToken = JwtToken,
                    RefreshToken = result
                };
                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
