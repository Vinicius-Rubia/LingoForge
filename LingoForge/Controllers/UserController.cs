using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Interfaces.UseCases.Users;
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

    [HttpDelete("{studentId:guid}")]
    [Authorize(Policy = AuthorizationPolicies.MustBeTeacher)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteStudent([FromServices] IDeleteStudentAccountUseCase useCase, [FromRoute] Guid studentId)
    {
        await useCase.Execute(studentId);
        return NoContent();
    }
}
