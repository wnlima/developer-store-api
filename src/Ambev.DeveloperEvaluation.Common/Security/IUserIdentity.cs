namespace Ambev.DeveloperEvaluation.Common.Security
{
    //Adicionar comentários para documentar esta classe em ingles    
    // /// <summary>
    /// Defines the contract for representing a user's identity within the system.
    /// This interface is used to access the user's unique identifier.
    /// </summary>
    public interface IUserIdentity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// This property is used to identify the related user in other entities.
        /// </summary>
        string UserId { get; set; }
    }
}
