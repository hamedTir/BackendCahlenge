using System.ComponentModel.DataAnnotations;

namespace ChalengeBackend.DTOs
{
    public class NotesDTO
    {
        [Required(ErrorMessage = "Text is required")]
        public string Content { get; set; } = String.Empty;
        [Required]
        public int UserId { get; set; }
    }
}
