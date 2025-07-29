using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Interfaces.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;

namespace LingoForge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseAuthenticatedUserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponseErrorDTO), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase, [FromBody] RequestLoginDTO request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
}
