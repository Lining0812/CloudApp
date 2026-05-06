using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Account
{
    public class RegisterRequest
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
        [Required]
        public required string Email { get; set; }
        public string? Password { get; set; }
    }
}
