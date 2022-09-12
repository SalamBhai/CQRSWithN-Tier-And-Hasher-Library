using MediatR;
using NubSkull.DTOs;
using NubSkull.DTOs.RequestModels;
using NubSkull.Interfaces.ICommands;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Commands;

public class UpdateUserCommand : IRequest<BaseResponse<UserDto>>
{
    public UpdateUserCommand(UpdateUserRequestModel updateUserRequestModel)
    {
        UpdateUserRequestModel = updateUserRequestModel;
    }

    public UpdateUserRequestModel UpdateUserRequestModel { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<UserDto>>
{
    private readonly IUserRepository  _userRepository;
    private readonly IUserRoleRepository  _userRoleRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<BaseResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
          var user = await _userRepository.GetUserById(request.UpdateUserRequestModel.Id);
          var userRoles =  await _userRoleRepository.GetAllUserRolesByUserId(user.Id);
      if(user == null)
      {
         return new BaseResponse<UserDto>
         {
            IsSuccessful = false,
            Message = "Role Update Failed, User To Update Not Found",
         };
      }
       user.UserName = request.UpdateUserRequestModel.UserName;
       user.EmailAddress = request.UpdateUserRequestModel.EmailAddress;
       await _userRepository.UpdateUser(user);
       return new BaseResponse<UserDto>
         {
            IsSuccessful = true,
            Message = "User Update Successful",
            Data = new UserDto
            {
                EmailAddress = user.EmailAddress,
                UserName = user.UserName,
                Id = user.Id,
                UserRoles = userRoles.Select(uR => uR.Role.Name).ToList()
            }
         };
    }
}