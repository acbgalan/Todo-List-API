using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Shared.User
{
    public class UserResponse
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}