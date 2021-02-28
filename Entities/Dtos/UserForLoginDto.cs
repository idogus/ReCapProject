using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserForLoginDto : IDto
    {
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}
