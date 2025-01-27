using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime? CreateDateTime { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedDateTime { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastLogin { get; set; } = DateTime.Now;

        public bool IsAdmin { get; set; } = false;
    }
}
