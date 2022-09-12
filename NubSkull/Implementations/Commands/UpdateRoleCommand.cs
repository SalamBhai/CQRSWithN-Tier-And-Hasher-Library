using MediatR;
using NubSkull.DTOs;
using NubSkull.Interfaces.ICommands;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Commands;

public class UpdateRoleCommand : IRequest<BaseResponse<RoleDto>>
{
    public UpdateRoleCommand(UpdateRoleRequestModel updateRoleRequestModel)
    {
        this.updateRoleRequestModel = updateRoleRequestModel;
    }

    public UpdateRoleRequestModel updateRoleRequestModel { get; set; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, BaseResponse<RoleDto>>
{
    private readonly IRoleRepository  _roleRepository;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<BaseResponse<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
      var role = await _roleRepository.GetRoleById(request.updateRoleRequestModel.Id);
      if(role == null)                
      {
         return new BaseResponse<RoleDto>
         {
            IsSuccessful = false,
            Message = "Role Update Failed",
         };
      }
       role.Name = request.updateRoleRequestModel.Name;
       role.Description = request.updateRoleRequestModel.Description;
       await _roleRepository.UpdateRoleAsync(role);
       return new BaseResponse<RoleDto>
         {
            IsSuccessful = true,
            Message = "Role Update Successful",
            Data = new RoleDto
            {
                Description = role.Description,
                Name = role.Name,
                RoleId = role.Id
            }
         };
    }
}