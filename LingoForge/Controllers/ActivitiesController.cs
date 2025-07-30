using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Interfaces.UseCases.Activities;
using LingoForge.Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoForge.Controllers;

[Route("api/classes/{classId:guid}/[controller]")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.MustBeTeacher)]
    [ProducesResponseType(typeof(ResponseActivityCreatedDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponseErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponseErrorDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(BaseResponseErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateActivity([FromServices] ICreateActivityUseCase useCase, [FromRoute] Guid classId, [FromBody] RequestCreateActivityDTO request)
    {
        var response = await useCase.Execute(classId, request);
        return Created(string.Empty, response);
    }
}
