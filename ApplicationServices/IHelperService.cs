using Core.Domain;
using Microsoft.AspNetCore.Http;

namespace ApplicationServices;

public interface IHelperService
{
    User GetUserById(int id);
    User GetUser(HttpContext context);
}