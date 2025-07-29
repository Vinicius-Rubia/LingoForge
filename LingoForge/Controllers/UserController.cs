using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Interfaces.UseCases.Auth;
using LingoForge.Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoForge.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class UserController : ControllerBase
{
    [HttpPost("create-account-for-student")]
    [Authorize(Policy = AuthorizationPolicies.MustBeTeacher)]
    [ProducesResponseType(typeof(ResponseCreatedUserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponseErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponseErrorDTO), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateAccountForStudent([FromServices] ICreateAccountUseCase useCase, [FromBody] RequestCreateAccountDTO request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
}
