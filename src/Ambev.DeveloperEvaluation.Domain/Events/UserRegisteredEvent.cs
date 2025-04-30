using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class UserRegisteredEvent
    {
        public UserEntity User { get; }

        public UserRegisteredEvent(UserEntity user)
        {
            User = user;
        }
    }
}
