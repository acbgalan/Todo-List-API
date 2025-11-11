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
        [StringLength(256, ErrorMessage = "The field {0} must contain {1} characters")]
        public required string Name { get; set; }
    }
}
