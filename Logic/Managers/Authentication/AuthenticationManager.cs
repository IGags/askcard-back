using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Settings.Models;
using Dal.User;
using Dal.User.Interfaces;
using Logic.Exceptions;
using Logic.Managers.Authentication.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Managers.Authentication;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IUserRepository _userRepository;
    private readonly IOptions<IdentitySettings> _options;

    public AuthenticationManager(IUserRepository userRepository,
         IOptions<IdentitySettings> options)
    {
        _userRepository = userRepository;
        _options = options;
    }
    
    public async Task<string> AuthAsync(AuthenticationModel model)
    {
        var user = await ValidateUserInternalAsync(model);
        var jwt = await AuthInternalAsync(user);
        return jwt;
    }

    private async Task<UserDal> ValidateUserInternalAsync(AuthenticationModel model)
    {
        UserDal userDal;
        try
        {
            var transaction = _userRepository.BeginTransaction();
            var dalResult = await _userRepository.GetByFieldAsync(nameof(UserDal.Email), model.Email, transaction);
            userDal = dalResult.Single();
        }
        catch (Exception)
        {
            throw new UserDoesNotExistsException();
        }

        var passwordHash = PasswordHashHelper.GetPasswordHash(model.Password);
        if (!userDal.PasswordHash.Equals(passwordHash))
        {
            throw new PasswordIsIncorrectException();
        }

        return userDal;
    }

    private async Task<string> AuthInternalAsync(UserDal user)
    {
        var claims = new List<Claim>();
        claims.Add(new("username", user.Email));
        claims.Add(new("displayName", user.Name));
        claims.Add(new(ClaimTypes.Role, user.Role));
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.Name));
        claims.Add(new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(_options.Value.Issuer, _options.Value.Audience, claims, null,
            DateTime.UtcNow.Add(TimeSpan.FromSeconds(_options.Value.TokenLifetime)), creds);
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}