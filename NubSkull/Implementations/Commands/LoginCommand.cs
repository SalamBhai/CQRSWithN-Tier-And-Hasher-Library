using MediatR;
using NubSkull.Authentication.Hasher;
using NubSkull.DTOs;
using NubSkull.Interfaces.ICommands;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Commands;

public class LoginCommand : IRequest<BaseResponse<UserDto>>
{
     public string EmailAddress {get; set;}
    public string Password {get; set;}
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseResponse<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IHasher _hasher;

    public LoginCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IHasher hasher)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _hasher = hasher;
    }

    public async Task<BaseResponse<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
         var user = await _userRepository.GetUserByEmail(request.EmailAddress);
         var userRoles = await _userRoleRepository.GetAllUserRolesByUserId(user.Id);
         var confirmPassword = _hasher.ValidatePassword(user.HashedPassword,request.Password);
         if(user == null || !confirmPassword)
         {
            return new BaseResponse<UserDto>
            {
                IsSuccessful = false,
                Message = "Invalid Email Address Or Password",
            };
         }

         return new BaseResponse<UserDto>
         {
           IsSuccessful = true,
           Message = "Login Successful",
           Data = new UserDto
           {
              EmailAddress = user.EmailAddress,
              Id = user.Id,
              UserName = user.UserName,
              UserRoles = userRoles.Select(uR => uR.Role.Name).ToList()
           }
         };
    }
}