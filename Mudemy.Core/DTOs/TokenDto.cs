using System;
using System.Collections.Generic;
using System.Text;

namespace Mudemy.Core.DTOs
{
    public class TokenDto
    {
        public string AccessToken { get; set; } = default!;

        public DateTime AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; } = default!;

        public DateTime RefreshTokenExpiration { get; set; }
    }
}