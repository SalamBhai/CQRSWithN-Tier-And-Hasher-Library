using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NubSkull.Context;
using NubSkull.DTOs;
using NubSkull.Identity;
using NubSkull.Interfaces.ICommands;

namespace NubSkull.Implementations.Commands;

public class RegisterRoleCommand : IRequest<BaseResponse>
{
 public string Name {get; set; }
  public string Description {get; set;}
}

public class RegisterRoleCommandHandler : IRequestHandler<RegisterRoleCommand, BaseResponse>
{
    private readonly ApplicationContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public RegisterRoleCommandHandler(ApplicationContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public async Task<BaseResponse> Handle(RegisterRoleCommand request, CancellationToken cancellationToken)
    {
       if (await _context.Roles.AnyAsync( role => role.Name == request.Name ))
       {
           return new BaseResponse
           {
              Message  = "Role Already Exists",
              IsSuccessful = false
           };
       }

       var role = new Role
       {
          Name = request.Name,
          Description = request.Description,
          CreatedOn = DateTime.UtcNow,
         // CreatedBy = int.Parse(_httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)),
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