using LingoForge.Domain.Enums;
using LingoForge.Domain.Exceptions;
using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Interfaces.UseCases.Users;
using LingoForge.Domain.Services;

namespace LingoForge.Application.UseCases.Users;

public class DeleteStudentAccountUseCase(
    IUserProvider userProvider,
    IUserRepository userRepository,
    ITurmaRepository turmaRepository,
    IUnityOfWork unityOfWork) : IDeleteStudentAccountUseCase
{
    private readonly IUserProvider _userProvider = userProvider;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITurmaRepository _turmaRepository = turmaRepository;

    public async Task Execute(Guid studentId)
    {
        var teacherIdentifier = _userProvider.GetUserIdentifier()
          ?? throw new UnauthorizedException();

        var studentToDelete = await _userRepository.GetByIdAsync(studentId)
            ?? throw new NotFoundException("Aluno não encontrado.");

        if (studentToDelete.Role != EUserRole.STUDENT)
            throw new BadRequestException("O usuário informado não é um aluno.");

        var studentClasses = await _turmaRepository.GetClassesByStudentIdAsync(studentId);

        bool isEnrolledWithOtherProfessor = studentClasses
           .Any(c => c.TeacherId != teacherIdentifier);

        if (isEnrolledWithOtherProfessor)
            throw new BadRequestException("Não é permitido deletar um aluno que está matriculado em turmas de outros professores.");

        _userRepository.Delete(studentToDelete);

        await _unityOfWork.Commit();
    }
}
