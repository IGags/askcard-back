using System.Threading.Tasks;
using Api.Controllers.PasswordRestore.Dto.Request;
using Api.Controllers.PasswordRestore.Dto.Response;
using Logic.Managers.PasswordRestore.Interfaces;
using Logic.Managers.PasswordRestore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PasswordRestore;

[ApiController]
[Route("api/v1/public/client/password-restore")]
public class PasswordRestoreController : Controller
{
    private readonly IRestorePasswordManager _manager;

    public PasswordRestoreController(IRestorePasswordManager manager)
    {
        _manager = manager;
    }
    
    [HttpPost("start")]
    public async Task<IActionResult> StartRestorePasswordAsync([FromBody] StartRestorePasswordRequest request)
    {
        var operationId = await _manager.StartRestorePassword(new RestorePasswordModel()
            { Email = request.Email, NewPassword = request.NewPassword });
        return Ok(new StartRestorePasswordResponse() { OperationName = operationId });
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmRestorePasswordAsync([FromBody] ConfirmRestorePasswordRequest request)
    {
        await _manager.CompleteRestorePassword(request.OperationId, request.Code);
        return Ok();
    }
}