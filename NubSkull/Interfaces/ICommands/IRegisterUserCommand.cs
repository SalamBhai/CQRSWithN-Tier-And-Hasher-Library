using MediatR;
using NubSkull.Context;
using NubSkull.DTOs;
using NubSkull.DTOs.RequestModels;
using NubSkull.Implementations.Commands;

namespace NubSkull.Interfaces.ICommands;

public interface IRegisterUserCommand : IRequest<BaseResponse>
{
    public CreateUserRequestModel CommandModel {get;set;}
}


