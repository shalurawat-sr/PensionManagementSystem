using System.ComponentModel.DataAnnotations;

namespace PensionManagementSystem.DTOs
{
    public class Register
    {
        [Key]

        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

    }
}
