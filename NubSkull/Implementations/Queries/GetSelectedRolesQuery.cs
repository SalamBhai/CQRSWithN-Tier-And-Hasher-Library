using MediatR;
using Microsoft.EntityFrameworkCore;
using NubSkull.Context;
using NubSkull.DTOs;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Queries;

public class GetSelectedRolesQuery :  IRequest<BaseResponse<IEnumerable<RoleDto>>>
{
    public GetSelectedRolesQuery(List<string> roleNames)
    {
        RoleNames = roleNames;
    }

    public List<string> RoleNames{get;set;} = new List<string>();
}

public class SelectedRolesQueryHandler : IRequestHandler<GetSelectedRolesQuery, BaseResponse<IEnumerable<RoleDto>>>
{
    private readonly ApplicationContext _context;
    private readonly IRoleRepository _roleRepository;

    public SelectedRolesQueryHandler(ApplicationContext context,IRoleRepository roleRepository)
    {
        _context = context;
        _roleRepository = roleRepository;
    }

    public async Task<BaseResponse<IEnumerable<RoleDto>>> Handle(GetSelectedRolesQuery request,  CancellationToken cancellationToken)
    {
        var selectedRoles  = await _roleRepository.GetSelectedRolesAsync(request.RoleNames);
         if(selectedRoles == null)
        {
            return new BaseResponse<IEnumerable<RoleDto>>
            {
               IsSuccessful = false,
               Message = "Role Retrieval Failed"
            };
        }
         var selectedRolesDto = selectedRoles.Select(role => new RoleDto
         {
              Description = role.Description,
              Name = role.Name,
              RoleId = role.Id
         }).ToList();
        return new BaseResponse<IEnumerable<RoleDto>>
        {
          Data = selectedRolesDto,
          IsSuccessful = true,
          Message = "Successful Retrieval Of Selected Roles"
        };
    }
}