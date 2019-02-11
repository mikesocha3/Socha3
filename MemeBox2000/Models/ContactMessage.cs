using System.ComponentModel.DataAnnotations;

namespace Socha3.MemeBox2000.Models
{
    public class ContactMessage
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

    }
}
