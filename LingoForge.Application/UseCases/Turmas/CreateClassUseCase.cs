using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Entities;
using LingoForge.Domain.Exceptions;
using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Interfaces.UseCases.Turmas;
using LingoForge.Domain.Services;

namespace LingoForge.Application.UseCases.Turmas;

public class CreateClassUseCase(
    IUserProvider userProvider,
    ITurmaRepository turmaRepository,
    IUnityOfWork unityOfWork) : ICreateClassUseCase
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IUserProvider _userProvider = userProvider;

    public async Task<ResponseClassCreatedDTO> Execute(RequestCreateClassDTO request)
    {
        var teacherIdentifier = _userProvider.GetUserIdentifier()
          ?? throw new UnauthorizedException();

        await Validate(request, teacherIdentifier);

        var turma = Turma.Create(request.Name, teacherIdentifier, request.Description);

        await _turmaRepository.AddAsync(turma);
        await _unityOfWork.Commit();

        return new ResponseClassCreatedDTO
        {
            Id = turma.Id,
            Name = turma.Name,
            Description = turma.Description,
            CreatedAt = turma.CreatedAt
        };
    }

    private async Task Validate(RequestCreateClassDTO request, Guid professorId)
    {
        var validationResult = await new CreateClassValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        var nameExists = await _turmaRepository.ExistsByNameAndProfessorIdAsync(request.Name, professorId);
        if (nameExists)
        {
            throw new ConflictException("Você já possui uma turma com este nome.");
        }
    }
}
