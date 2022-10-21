using ApplicationServices;

namespace WebService.GraphQL;

public class Mutation
{
    public Task<string> GetToken(string email, string password, [Service] IIdentityService<string> identityService)
    {
        return identityService.SignIn(new AuthenticationCredentials(email, password));
    }
}