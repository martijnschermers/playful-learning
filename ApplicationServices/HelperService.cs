using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Http;

namespace ApplicationServices;

public class HelperService : IHelperService
{
    private readonly IUserService _userService;
    
    public HelperService(IUserService userService)
    {
        _userService = userService;
    }
    
    public User GetUser(HttpContext context)
    {
        var identity = context.User.Identity;
        return _userService.GetUserByEmail(identity!.Name!);
    }
}