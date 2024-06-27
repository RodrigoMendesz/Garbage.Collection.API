using Garbage.Collection.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Business.Service.Interfaces
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
    }
}
