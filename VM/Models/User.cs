#nullable disable warnings

using System.ComponentModel.DataAnnotations;

namespace VM.Models;

public class User
{
    [EmailAddress]
    [Required(ErrorMessage = "Please insert a valid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please insert a valid phone number.")]
    public string PhoneNumber { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please insert a valid password.")]
    [StringLength(100, ErrorMessage = "The password's length is invalid.", MinimumLength = 6)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please insert a valid password.")]
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; }
}
