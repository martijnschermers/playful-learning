using Core.Domain;
using Microsoft.AspNetCore.Http;

namespace ApplicationServices;

public interface IHelperService
{
    User GetUser(HttpContext context);
}