using MediatR;
using NubSkull.DTOs;
using NubSkull.Interfaces.IRepositories;
namespace NubSkull.Implementations.Queries;

public class GetAllUsersQuery :  IRequest<BaseResponse<IEnumerable<UserDto>>>
{

}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, BaseResponse<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<BaseResponse<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers();
        var userRoles = await _userRoleRepository.GetAllUserRoles();
        var usersDto = users.Select(users => new UserDto
        {
          EmailAddress = users.EmailAddress,
          Id = users.Id,
          UserName = users.UserName,
          UserRoles = userRoles.Select(uR => uR.Role.Name).ToList()
        }).ToList();
        if(users == null)
        {
            return new BaseResponse<IEnumerable<UserDto>>
            {
               IsSuccessful = false,
               Message = "User Retrieval Failed"
            };
        }
        return new BaseResponse<IEnumerable<UserDto>>
        {
            Data = usersDto,
            IsSuccessful = true,
            Message = "Retrieval Successful",
        };
    }
}
