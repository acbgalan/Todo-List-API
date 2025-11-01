using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Data.Entities
{
    public class Todo
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field is required")]
        [StringLength(50, ErrorMessage = "The field cannot exceed {1} characters")]
        public required string Title { get; set; }

        [StringLength(500, ErrorMessage = "The field cannot exceed {1} characters")]
        public string Description { get; set; } = string.Empty;

        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public IdentityUser? User { get; set; }
    }
}
