using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApplicationServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebService.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    
    public AuthenticationController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    [HttpPost("api/signin")]
    public async Task<IActionResult> SignIn([FromBody] AuthenticationCredentials authenticationCredentials)
    {
        var user = await _userManager.FindByNameAsync(authenticationCredentials.Email);
        
        if (user == null) return BadRequest();

        if (!(await _signInManager.PasswordSignInAsync(user, authenticationCredentials.Password, false, false)).Succeeded) return BadRequest();
        
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"]
        };

        var handler = new JwtSecurityTokenHandler();
        var securityToken = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);

        return Ok(new { Succes = true, Token = handler.WriteToken(securityToken) });
    }

    [HttpPost("api/signout")]
    public new async Task<IActionResult> SignOut() 
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Succes = true });
    }
}