using Bussines.Abstract;
using Core.Entity.Models;
using Core.Security.Hashing;
using Core.Security.Jwt;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserManager _userManager;

        public AuthManager(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public string Login(LoginDTO loginDTO)
        {
            var cheackUser = _userManager.GetByEmail(loginDTO.Email);
            if (cheackUser == null)
             return null;
            var cheackPassword = HashingHelper.VerifyPassword(loginDTO.Password, cheackUser.PasswordHash, cheackUser.PasswordSalt);
            if (!cheackPassword)
             return null;

            var token = TokenGenerator.Token(cheackUser, "User");
            return token;
        }

        public void Register(RegisterDTO registerDTO)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.HashPassword(registerDTO.Password, out passwordHash, out passwordSalt);
            User user = new()
            {
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Surname = registerDTO.Surname,

            };
            _userManager.Add(user);
        }
    }
}
