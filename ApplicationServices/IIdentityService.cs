using WebService.Models;

namespace ApplicationServices;

public interface IIdentityService<T>
{
    Task<T> SignIn(AuthenticationCredentials authenticationCredentials);
    Task<T> SignOut();
}