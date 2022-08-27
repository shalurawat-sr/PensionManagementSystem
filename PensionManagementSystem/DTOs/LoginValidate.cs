using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementSystem.DTOs
{
    public class LoginValidate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
