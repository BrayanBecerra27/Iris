using IrisCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisCore.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserLoginDTO userLogin);
    }
}
