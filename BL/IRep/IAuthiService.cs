using Api_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.BL.IRep
{
    public interface IAuthiService
    {
        Task<AuthenticationModel> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenModel model);
        Task<string> addRoleAsync(AddRoleModel model);
    }
}
