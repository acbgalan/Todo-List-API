using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Shared.User
{
    public class UserLoginResponse
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
