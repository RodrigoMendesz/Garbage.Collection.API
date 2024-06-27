using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Business.Service
{
    public class AuthService  : IAuthService
    {
        private List<User> _users = new List<User>
                {
                    new User { UserId = 1, Username = "adm", Password = "adm123", Role = "Administrador" },
                    new User { UserId = 2, Username = "rodrigo", Password = "12345", Role = "Cliente" },
                };


        public User Authenticate(string username, string password)
        {
            // Aqui você normalmente faria a verificação de senha de forma segura
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
