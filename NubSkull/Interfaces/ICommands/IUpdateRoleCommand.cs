using MediatR;
using NubSkull.DTOs;

namespace NubSkull.Interfaces.ICommands;

public interface IUpdateRoleCommand 
{
    public UpdateRoleRequestModel updateRoleRequestModel {get;set; }
}
