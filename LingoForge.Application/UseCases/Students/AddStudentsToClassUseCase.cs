using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.Exceptions;
using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Interfaces.UseCases.Students;
using LingoForge.Domain.Services;

namespace LingoForge.Application.UseCases.Students;

public class AddStudentsToClassUseCase(
    ITurmaRepository turmaRepository,
    IUserRepository userRepository,
    IUserProvider userProvider,
    IUnityOfWork unityOfWork) : IAddStudentsToClassUseCase
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IUserProvider _userProvider = userProvider;

    public async Task Execute(Guid classId, RequestAddStudentsToClassDTO request)
    {
        var teacherIdentifier = _userProvider.GetUserIdentifier()
          ?? throw new UnauthorizedException();

        await Validate(request);

        var turma = await _turmaRepository.GetByIdWithEnrollmentsAsync(classId)
            ?? throw new NotFoundException("Turma não encontrada.");

        if (turma.TeacherId != teacherIdentifier)
            throw new ForbiddenException("Você não tem permissão para modificar esta turma.");

        var studentsToAdd = await _userRepository.GetStudentsByIdsAsync(request.StudentIds);

        if (studentsToAdd.Count != request.StudentIds.Count)
        {
            throw new BadRequestException("Um ou mais IDs de alunos são inválidos ou não encontrados.");
        }

        foreach (var student in studentsToAdd)
        {
            turma.AddStudent(student);
        }

        await _unityOfWork.Commit();
    }

    private async static Task Validate(RequestAddStudentsToClassDTO request)
    {
        var validationResult = await new AddStudentsToClassValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
