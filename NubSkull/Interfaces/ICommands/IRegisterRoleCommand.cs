using MediatR;
using NubSkull.DTOs;
using NubSkull.Implementations.Commands;

namespace NubSkull.Interfaces.ICommands;

public interface IRegisterRoleCommand : IRequest<BaseResponse>
{
   public CreateRoleRequestModel CreateRoleRequestModel {get; set;}
}

