using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementSystem.Models
{
    //[Index(nameof(PensionId), IsUnique = true)]

    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int RequestId { get; set; }
        public string PensionId { get; set; }
        public string PensionName { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }

    }
}
