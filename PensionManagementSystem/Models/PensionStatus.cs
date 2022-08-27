using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementSystem.Models
{
    public class PensionStatus
    {

        [Key]
        public string PensionId { get; set; }
        public string? Email { get; set; }
        [ForeignKey("Email")]
        public User Users { get; set; }
        public string? PensionName { get; set; }
        public string? Status { get; set; }
        public string? Active { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? PensionReleaseDate { get; set; }
        

    }
}
