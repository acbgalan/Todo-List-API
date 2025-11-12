using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Shared.User
{
    public class UserRegisterRequest : CredentialsRequest
    {
        [Required]
        public required string Name { get; set; }
    }
}
