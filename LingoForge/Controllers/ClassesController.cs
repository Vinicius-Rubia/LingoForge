using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.Interfaces.UseCases.Students;
using LingoForge.Domain.Interfaces.UseCases.Turmas;
using LingoForge.Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoForge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassesController : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.MustBeTeacher)]
    public async Task<IActionResult> CreateClass([FromServices] ICreateClassUseCase useCase, [FromBody] RequestCreateClassDTO request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpPost("{classId:guid}/students")]
    [Authorize(Policy = AuthorizationPolicies.MustBeTeacher)]
    public async Task<IActionResult> AddStudentsToClass([FromServices] IAddStudentsToClassUseCase useCase, [FromBody] RequestAddStudentsToClassDTO request, [FromRoute] Guid classId)
    {
        await useCase.Execute(classId, request);
        return NoContent();
    }

    [HttpDelete("{classId:guid}/students/{studentId:guid}")]
    [Authorize(Policy = AuthorizationPolicies.MustBeTeacher)]
    public async Task<IActionResult> RemoveStudentFromClass([FromServices] IRemoveStudentFromClassUseCase useCase, Guid classId, Guid studentId)
    {
        await useCase.Execute(classId, studentId);
        return NoContent();
    }
}
