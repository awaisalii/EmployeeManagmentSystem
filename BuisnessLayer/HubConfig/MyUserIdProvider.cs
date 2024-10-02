using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

public class MyUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        // Extract the user ID from the NameIdentifier claim (usually from the JWT token)
        return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
