using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebService.Models;

namespace ApplicationServices;

public class IdentityService : IIdentityService<string>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    
    public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task<string> SignIn(AuthenticationCredentials authenticationCredentials)
    {
        var user = await _userManager.FindByNameAsync(authenticationCredentials.Email);
        
        if (user == null) return "User not found";

        if (!(await _signInManager.PasswordSignInAsync(user, authenticationCredentials.Password, false, false)).Succeeded) return "Invalid credentials";
        
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"]
        };

        var handler = new JwtSecurityTokenHandler();
        var securityToken = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);

        return handler.WriteToken(securityToken);
    }

    public Task<string> SignOut()
    {
        throw new NotImplementedException();
    }
}