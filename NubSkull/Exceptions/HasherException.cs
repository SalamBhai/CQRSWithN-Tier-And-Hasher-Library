namespace NubSkull.Exceptions;

public class HasherException : Exception
{
    public new string Message = "Invalid Salt Version or Password";
}
