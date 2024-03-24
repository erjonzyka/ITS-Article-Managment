using ITS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Interfaces
{
    public interface ILoginService
    {
        User GetUser(string email);
        bool UserExists(string email);
        bool PasswordCheck(UserLogin user,string passwordOne, string passwordTwo);
        bool SaveUser(User user);
    }
}
