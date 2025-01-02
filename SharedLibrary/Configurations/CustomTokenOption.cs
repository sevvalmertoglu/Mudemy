using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Configurations
{
    public class CustomTokenOption
    {
        public required List<String> Audience { get; set; }
        public required string Issuer { get; set; }

        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }

        public required string SecurityKey { get; set; }
    }
}