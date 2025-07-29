using LingoForge.Domain.Exceptions;
using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Interfaces.UseCases.Students;
using LingoForge.Domain.Services;

namespace LingoForge.Application.UseCases.Students;

public class RemoveStudentFromClassUseCase(
    IUserProvider userProvider,
    ITurmaRepository turmaRepository,
    IUnityOfWork unityOfWork) : IRemoveStudentFromClassUseCase
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IUserProvider _userProvider = userProvider;

    public async Task Execute(Guid classId, Guid studentId)
    {
        var teacherIdentifier = _userProvider.GetUserIdentifier()
         ?? throw new UnauthorizedException();

        var turma = await _turmaRepository.GetByIdWithEnrollmentsAsync(classId)
            ?? throw new NotFoundException("Turma não encontrada.");

        if (turma.TeacherId != teacherIdentifier)
            throw new ForbiddenException("Você não tem permissão para modificar esta turma.");

        turma.RemoveStudent(studentId);

        await _unityOfWork.Commit();
    }
}
