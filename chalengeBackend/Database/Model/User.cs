using System.ComponentModel.DataAnnotations;

namespace ChalengeBackend.Database.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; } = String.Empty;
        [MaxLength(128)]
        public string LastName { get; set; } = String.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        [Range(0, 128)]
        public int Age { get; set; }
        public string Website { get; set; } = String.Empty;
    }

}
