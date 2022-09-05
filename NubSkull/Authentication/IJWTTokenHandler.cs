using NubSkull.DTOs;

namespace NubSkull.Authentication;

public interface IJWTTokenHandler
{
    string GenerateToken(UserDto userDto);
}
