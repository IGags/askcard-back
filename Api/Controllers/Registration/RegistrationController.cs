using System.Threading.Tasks;
using Api.Controllers.Registration.Dto.Request;
using Api.Controllers.Registration.Dto.Response;
using Logic.Managers.Registration;
using Logic.Managers.Registration.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Registration;

/// <summary>
/// Контроллер регистрации пользователей
/// </summary>
[ApiController]
[Route("api/v1/public/client/register")]
public class RegistrationController : Controller
{
    private readonly IRegistrationManager _registrationManager;

    public RegistrationController(IRegistrationManager registrationManager)
    {
        _registrationManager = registrationManager;
    }
    
    [HttpPost("start")]
    [ProducesResponseType(typeof(StartRegistrationResponse), 200)]
    public async Task<IActionResult> StartRegistrationAsync([FromBody]StartRegistrationRequest request)
    {
        var userModel = new CreateUserModel()
        {
            Email = request.Email,
            IsAgree = request.IsAgree.Value,
            Login = request.Login,
            Password = request.Password
        };
        var id = await _registrationManager.StartRegistration(userModel);

        var response = new StartRegistrationResponse()
        {
            OperationId = id
        };
        
        return Ok(response);
    }

    [HttpPost("confirm")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> ConfirmRegistrationAsync([FromBody]ConfirmRegistrationRequest request)
    {
        var model = new ConfirmUserModel()
        {
            Code = request.Code,
            OperationId = request.OperationId.Value
        };

        await _registrationManager.ConfirmRegistration(model);

        return Ok();
    }
}