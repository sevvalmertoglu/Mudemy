using System;
using System.Collections.Generic;
using System.Text;
using Mudemy.Core.DTOs;
using Mudemy.Core.Models;

namespace Mudemy.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);
    }
}