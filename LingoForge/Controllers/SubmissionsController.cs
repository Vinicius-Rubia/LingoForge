using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.Interfaces.UseCases.Answers;
using LingoForge.Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoForge.Controllers;

[Route("api/activities/{activityId:guid}/[controller]")]
[ApiController]
public class SubmissionsController : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.MustBeStudent)]
    public async Task<IActionResult> SubmitActivity(
        [FromServices] ISubmitActivityUseCase useCase,
        [FromRoute] Guid activityId,
        [FromBody] RequestSubmitActivityDTO request)
    {
        var response = await useCase.Execute(activityId, request);
        return Created(string.Empty, response);
    }
}
