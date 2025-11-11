using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public required string Name { get; set; }
    }
}
