using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChalengeBackend.Database.Model
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Text is required")]
        public string Content { get; set; } = String.Empty;
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        public bool Published { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        // Navigation property to User
        [JsonIgnore]
        public User User { get; set; } 
    }
}


