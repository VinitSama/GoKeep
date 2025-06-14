using GoKeep.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository
{
    public class UserRL : IUserRL
    {
        private readonly DatabaseContext _context;

        public UserRL(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUser(RegisterRequestModel userModel, string passwordHash)
        {
            Console.WriteLine("3");
            try
            {

                var userWithEmail = await _context.UsersKeep
                    .FirstOrDefaultAsync(u => u.Email == userModel.Email);
                if (userWithEmail != null)
                {
                    // Email already exists
                    if (userWithEmail.IsActive)
                    {
                        return false; // User already exists and is active
                    }
                    userWithEmail.IsActive = true; // Reactivate if it exists
                    userWithEmail.PasswordHash = passwordHash;
                    userWithEmail.FirstName = userModel.FirstName;
                    userWithEmail.LastName = userModel.LastName;
                    userWithEmail.UpdatedAt = DateTime.UtcNow;
                    _context.UsersKeep.Update(userWithEmail);
                    await _context.SaveChangesAsync();
                    return true;
                }
                var newUser = new UsersKeepEntity
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Email = userModel.Email,
                    PasswordHash = passwordHash,
                    IsActive = true
                };
                var result = await _context.UsersKeep.AddAsync(newUser);
                if (result != null)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginResponseModel> LoginUser(LoginRequestModel userModel)
        {
            try
            {
                var response = await _context.UsersKeep.FirstOrDefaultAsync(r => r.Email == userModel.Email && r.IsActive);
                if (response == null)
                {
                    return null!;
                }
                Console.WriteLine(response);
                //var refreshToken = new RefreshCreateRequestModel(response.Id, Guid.NewGuid().ToString(), 7);
                var refreshResponse = await _context.UserRefreshTokens
                    .FirstOrDefaultAsync(r => r.UserId == response.Id && r.IsActive);
                if (refreshResponse != null)
                {
                    refreshResponse.IsActive = false;
                }
                var refreshToken = new RefreshTokenEntity
                {
                    UserId = response.Id,
                    ExpiryDate = DateTime.UtcNow.AddDays(7),
                    Token = Guid.NewGuid().ToString(),
                    IsActive = true
                };
                await _context.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
                return new LoginResponseModel()
                {
                    Email = userModel.Email,
                    Password = response.PasswordHash,
                    RefreshToken = refreshToken.Token
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> RefreshToken(RefreshTokenModel refreshModel)
        {
            try
            {
                var userResponse = await _context.UsersKeep
                    .FirstOrDefaultAsync(r => r.Email == refreshModel.Email && r.IsActive);
                Console.WriteLine(userResponse?.Id + " " +userResponse?.Email+ " "+userResponse?.CreatedAt);
                if (userResponse == null)
                {
                    return null!;
                }
                var refreshResponse = await _context.UserRefreshTokens
                    .FirstOrDefaultAsync(r => r.Token == refreshModel.OldToken && r.IsActive);
                //Console.WriteLine(refreshResponse?.CreatedAt + " " + refreshResponse.ExpiryDate);
                if (refreshResponse == null)
                {
                    return null!;
                }
                refreshResponse.IsActive = false;

                if (refreshResponse.ExpiryDate < DateTime.Now)
                {
                    await _context.SaveChangesAsync();
                    return null!;
                }
                var newToken = Guid.NewGuid().ToString();
                var refreshToken = new RefreshTokenEntity()
                {
                    UserId = userResponse.Id,
                    ExpiryDate = DateTime.UtcNow.AddDays(7),
                    Token = newToken,
                    IsActive = true
                };

                await _context.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
                return newToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}