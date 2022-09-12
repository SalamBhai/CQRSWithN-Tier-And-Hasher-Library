using MediatR;
using NubSkull.DTOs;
using NubSkull.DTOs.RequestModels;

namespace NubSkull.Interfaces.ICommands;

public interface IUpdateUserCommand : IRequest<BaseResponse<UserDto>>
{
    public UpdateUserRequestModel UpdateUserRequestModel {get;set;}
}
