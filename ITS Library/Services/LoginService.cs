using ITS_Library.Interfaces;
using ITS_Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Services
{
    public class LoginService : ILoginService
    {
        public MyContext _context { get; }
        public LoginService(MyContext context) {
            _context = context;
        }

        public User GetUser(string email)
        {
            User? CurrentUser = _context.Users.FirstOrDefault(e => e.Email == email);
            return CurrentUser;
        }

       

        public bool UserExists(string email)
        {
            User? CurrentUser = _context.Users.FirstOrDefault(e => e.Email == email);
            if (CurrentUser == null)
            {
                return false;
            }
            return true;
        }

        public bool PasswordCheck(UserLogin user,string passwordOne, string passwordTwo)
        {
            PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
            var result = hasher.VerifyHashedPassword(user, passwordOne, passwordTwo);
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public bool SaveUser(User user)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Add(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
