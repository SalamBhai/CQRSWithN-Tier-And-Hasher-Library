using MediatR;
using NubSkull.DTOs;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Queries;

public class GetUserByEmailQuery :  IRequest<BaseResponse<UserDto>>
{
    public GetUserByEmailQuery(string emailAddress)
    {
        EmailAddress = emailAddress;
    }

    public string EmailAddress { get; set; }
}

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, BaseResponse<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<BaseResponse<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.EmailAddress);
        var userRoles = await _userRoleRepository.GetAllUserRolesByUserId(user.Id);
         if(user == null)
        {
            return new BaseResponse<UserDto>
            {
               IsSuccessful = false,
               Message = "User Retrieval Failed"
            };
        }
        return new BaseResponse<UserDto>
        {
            Data = new UserDto
            {
                EmailAddress = user.EmailAddress,
                Id = user.Id,
                UserName = user.UserName,
                UserRoles = userRoles.Select(uR => uR.Role.Name).ToList(),
            },
            IsSuccessful = true,
            Message = "User By Email Successfully Retrieved",

        };
    }
}