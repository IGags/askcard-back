using System.Threading.Tasks;
using Api.Controllers.Authentication.Dto;
using Logic.Managers.Authentication;
using Logic.Managers.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authentication;

[ApiController]
[AllowAnonymous]
[Route("api/v1/public/client/auth")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationManager _manager;

    public AuthenticationController(IAuthenticationManager manager)
    {
        _manager = manager;
    }
    
    [ProducesResponseType(typeof(AuthenticationResponse), 200)]
    [HttpPost]
    public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticationRequest request)
    {
        var model = new AuthenticationModel()
        {
            Email = request.Email,
            Password = request.Password
        };

        var jwt = await _manager.AuthAsync(model);

        var response = new AuthenticationResponse()
        {
            AccessToken = jwt
        };

        return Ok(response);
    }
}