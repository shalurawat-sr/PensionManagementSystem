using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementSystem.Models
{
    public class User
    {
       
        [Key]
        [Required]
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BankType { get; set; }
        public string? Aadhar { get; set; }
        public string? PAN { get; set; }


    }
}
