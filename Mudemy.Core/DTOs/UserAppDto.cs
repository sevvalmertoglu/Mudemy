using System;
using System.Collections.Generic;
using System.Text;

namespace Mudemy.Core.DTOs
{
    public class UserAppDto
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}