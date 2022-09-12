using MediatR;
using NubSkull.DTOs;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Queries;

public class GetRoleByNameQuery :  IRequest<BaseResponse<RoleDto>>
{
    public GetRoleByNameQuery(string roleName)
    {
        RoleName = roleName;
    }

    public string RoleName { get; set; }
}

public class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, BaseResponse<RoleDto>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleByNameQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<BaseResponse<RoleDto>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetRoleByName(request.RoleName);
        if(role == null)
        {
            return new BaseResponse<RoleDto>
            {
               IsSuccessful = false,
               Message = "Role Retrieval Failed"
            };
        }
        
        return new BaseResponse<RoleDto>
        {
            IsSuccessful = true,
            Message = "Role By Name Gotten",
            Data = new RoleDto
            {
                Description = role.Description,
                Name = role.Name,
                RoleId = role.Id,
            }
        };
    }
}