using System;
using System.Collections.Generic;
using System.Text;

namespace Mudemy.Core.DTOs
{
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}