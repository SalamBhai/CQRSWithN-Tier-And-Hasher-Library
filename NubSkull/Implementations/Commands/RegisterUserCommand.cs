using MediatR;
using Microsoft.EntityFrameworkCore;
using NubSkull.Context;
using NubSkull.DTOs;
using NubSkull.DTOs.RequestModels;
using NubSkull.Identity;
using NubSkull.Interfaces.ICommands;

namespace NubSkull.Implementations.Commands;

public class RegisterUserCommand : IRegisterUserCommand
{
    public CreateUserRequestModel CommandModel { get ; set;}
}

public class RegisterUserCommandHandler : IRequestHandler<IRegisterUserCommand, BaseResponse>
{
    private readonly ApplicationContext _context;

    public RegisterUserCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse> Handle(IRegisterUserCommand request, CancellationToken cancellationToken)
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
          
       };
    }
}