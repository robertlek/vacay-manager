#nullable disable warnings

using System.ComponentModel.DataAnnotations;

namespace VM.Models;

public class User
{
    /// <summary>
    /// Represents the email address of a user.
    /// </summary>
    [EmailAddress]
    [Required(ErrorMessage = "Please insert a valid email address.")]
    public string Email { get; set; }

    /// <summary>
    /// Represents the phone number of a user.
    /// </summary>
    [Required(ErrorMessage = "Please insert a valid phone number.")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Represents the password of a user's account.
    /// </summary>
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please insert a valid password.")]
    [StringLength(100, ErrorMessage = "The password's length is invalid.", MinimumLength = 6)]
    public string Password { get; set; }

    /// <summary>
    /// An extra property that is used to validate the password of a user's account.
    /// </summary>
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please insert a valid password.")]
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; }
}
