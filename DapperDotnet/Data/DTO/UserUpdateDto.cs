using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class UserUpdateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; } = string.Empty;
    }
}
