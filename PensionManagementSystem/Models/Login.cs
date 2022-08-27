using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementSystem.Models
{
    public class Login
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey("Email")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Roles { get; set; }

    }
}
