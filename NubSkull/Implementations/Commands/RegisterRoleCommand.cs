using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NubSkull.Context;
using NubSkull.DTOs;
using NubSkull.Identity;
using NubSkull.Interfaces.ICommands;

namespace NubSkull.Implementations.Commands;

public class RegisterRoleCommand : IRegisterRoleCommand
{
    public CreateRoleRequestModel CreateRoleRequestModel { get; set; }
}

public class RegisterRoleCommandHandler : IRequestHandler<IRegisterRoleCommand, BaseResponse>
{
    private readonly ApplicationContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public RegisterRoleCommandHandler(ApplicationContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public async Task<BaseResponse> Handle(IRegisterRoleCommand request, CancellationToken cancellationToken)
    {
       if (await _context.Roles.AnyAsync( role => role.Name == request.CreateRoleRequestModel.Name ))
       {
           return new BaseResponse
           {
              Message  = "Role Already Exists",
              IsSuccessful = false
           };
       }

       var role = new Role
       {
          Name = request.CreateRoleRequestModel.Name,
          Description = request.CreateRoleRequestModel.Description,
          CreatedOn = DateTime.UtcNow,
          CreatedBy = int.Parse(_httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)),
       };
       await _context.Roles.AddAsync(role);
       await _context.SaveChangesAsync();
       return new BaseResponse
       {
          IsSuccessful = true,
          Message = "Role Registration Successful"
       };
    }
}