using MediatR;
using NubSkull.DTOs;

namespace NubSkull.Interfaces.ICommands;

public interface ILoginCommand : IRequest<BaseResponse<UserDto>>
{
    public LoginRequestModel _loginRequestModel {get; set;}
}
