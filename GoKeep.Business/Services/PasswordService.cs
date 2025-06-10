using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public class PasswordService
    {
        //private readonly PasswordHasher<object> _passwordHasher;

        //public PasswordService(PasswordHasher<object> passwordHasher)
        //{
        //    _passwordHasher = passwordHasher;
        //}
        public static string HashPassword(string plaintText)
        {
            var _passwordHasher = new PasswordHasher<object>();
            return _passwordHasher.HashPassword(null, plaintText);
        }
        public static bool VerifyPassword(string storedPassword, string enteredPassword)
        {
            var _passwordHasher = new PasswordHasher<object>();
            return _passwordHasher.VerifyHashedPassword(null, storedPassword, enteredPassword) == PasswordVerificationResult.Success;
        }
    }
}
