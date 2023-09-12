using System.ComponentModel.DataAnnotations;

namespace JwtAuthMicroservice.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$", ErrorMessage = "The password must have at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }
    }

}
