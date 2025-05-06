using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public UserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<CreateUserResult, UserResponse>();
        CreateMap<UserResult, UserResponse>();
        CreateMap<Guid, Application.Users.DeleteUser.DeleteUserCommand>()
            .ConstructUsing(id => new Application.Users.DeleteUser.DeleteUserCommand(id));
        CreateMap<Guid, GetUserCommand>()
            .ConstructUsing(id => new GetUserCommand(id));

        CreateMap<UserResult, GetUserResponse>();
    }
}
