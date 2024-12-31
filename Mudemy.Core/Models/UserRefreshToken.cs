using System;
using System.Collections.Generic;
using System.Text;

namespace Mudemy.Core.Models
{
    public class UserRefreshToken
    {
        public string UserId { get; set; } = default!;
        public string Code { get; set; } = default!;
        public DateTime Expiration { get; set; }
    }
}