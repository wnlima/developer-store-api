namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

public interface IAuthenticateUserFields
{
    /// <summary>
    /// Gets or sets the email address for authentication.
    /// Used as the primary identifier for the user.
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// Gets or sets the password for authentication.
    /// Will be verified against the stored hashed password.
    /// </summary>
    string Password { get; set; }
}
