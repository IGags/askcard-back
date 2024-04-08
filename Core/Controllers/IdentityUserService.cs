using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Controllers;

public class IdentityUserService
{
    private readonly IHttpContextAccessor _accessor;

    public IdentityUserService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Guid GetUserId()
    {
        var token = _accessor.HttpContext.Request.Headers.Authorization.ToString().Split(' ')[1];
        var jwt = new JwtSecurityToken(token);
        return Guid.Parse(jwt.Subject);
    }
}