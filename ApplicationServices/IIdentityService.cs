namespace ApplicationServices;

public interface IIdentityService<T>
{
    Task<T> SignIn(AuthenticationCredentials authenticationCredentials);
    void SignOut();
}