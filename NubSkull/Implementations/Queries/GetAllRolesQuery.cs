using MediatR;
using NubSkull.DTOs;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Queries;

public class GetAllRolesQuery :  IRequest<BaseResponse<IEnumerable<RoleDto>>>
{
}


public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, BaseResponse<IEnumerable<RoleDto>>>
{
    private readonly IRoleRepository _roleRepository;

    public GetAllRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<BaseResponse<IEnumerable<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var allRoles = await _roleRepository.GetAllRoles();
        if (allRoles == null)
        {
            return new BaseResponse<IEnumerable<RoleDto>>
            {
                IsSuccessful = false,
                Message = "Roles Retrieval Failed"
            };
        }
        var selectedRolesDto = allRoles.Select(role => new RoleDto
        {
            Description = role.Description,
            Name = role.Name,
            RoleId = role.Id
        }).ToList();
        return new BaseResponse<IEnumerable<RoleDto>>
        {
            Data = selectedRolesDto,
            IsSuccessful = true,
            Message = "Successful Retrieval Of All Roles"
        };
    }
}