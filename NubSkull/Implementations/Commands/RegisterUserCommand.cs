using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NubSkull.Authentication.Hasher;
using NubSkull.Context;
using NubSkull.DTOs;
using NubSkull.DTOs.RequestModels;
using NubSkull.Identity;
using NubSkull.Implementations.Queries;
using NubSkull.Interfaces.ICommands;
using NubSkull.Interfaces.IRepositories;

namespace NubSkull.Implementations.Commands;

public class RegisterUserCommand : IRequest<BaseResponse>
{
    public RegisterUserCommand(CreateUserRequestModel commandModel)
    {
        CommandModel = commandModel;
    }

    public CreateUserRequestModel CommandModel { get ; set;}
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse>
{
    private readonly ApplicationContext _context;
    private readonly IHasher _hasher;
     private readonly IHttpContextAccessor _httpContextAccessor;
     private readonly IUserRoleRepository _userRoleRepository;
     private readonly IUserRepository _userRepository;
     private readonly IRoleRepository _roleRepository;

    public RegisterUserCommandHandler(ApplicationContext context, IHasher hasher,
     IHttpContextAccessor httpContextAccessor, IUserRoleRepository userRoleRepository, 
     IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _context = context;
        _hasher = hasher;
        _httpContextAccessor = httpContextAccessor;
        _userRoleRepository = userRoleRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<BaseResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
       if(await _context.Users.AnyAsync(user => user.UserName == request.CommandModel.UserName))
       {
          return new BaseResponse
          {
               IsSuccessful = true,
               Message = "User Exists Already"
          };
       }

       var user = new User
       {
          UserName = request.CommandModel.UserName,
          EmailAddress = request.CommandModel.EmailAddress,
          NormalizedPassword = request.CommandModel.Password,
          HashedPassword = _hasher.GenerateHash(request.CommandModel.Password),
          //CreatedBy = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)),
          CreatedOn = DateTime.UtcNow,
       };
      var createdUser = await _userRepository.CreateUser(user);
       var roles = await _roleRepository.GetSelectedRolesAsync(request.CommandModel.RoleNames);
       foreach (var role in roles)
       {
          var userRole = new UserRole
          {
            RoleId = role.Id,
            UserId = createdUser.Id,
            //CreatedBy = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)),
            CreatedOn = DateTime.UtcNow, 
          };
          createdUser.UserRoles.Add(userRole);
           await _userRoleRepository.AddUserRoleAsync(userRole);
       }
       var userTwo = await _userRepository.UpdateUser(createdUser);
    
       return new BaseResponse
       {
         IsSuccessful = true,
          Message = "Successful User Creation",
       };
    
    }
}           