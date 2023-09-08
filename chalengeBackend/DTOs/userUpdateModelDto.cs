using System.ComponentModel.DataAnnotations;

namespace ChalengeBackend.DTOs
{
    public class userUpdateModelDto
    {
        public string FirstName { get; set; } = String.Empty;
        [MaxLength(128)]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        [Range(0, 128)]
        public int Age { get; set; }
        public string? Website { get; set; }
    }
}
