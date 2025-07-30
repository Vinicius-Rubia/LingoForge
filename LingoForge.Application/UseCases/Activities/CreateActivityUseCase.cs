using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Entities;
using LingoForge.Domain.Enums;
using LingoForge.Domain.Exceptions;
using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Interfaces.UseCases.Activities;
using LingoForge.Domain.Services;

namespace LingoForge.Application.UseCases.Activities;

public class CreateActivityUseCase(
   IActivityRepository activityRepository,
   IUserProvider userProvider,
   ITurmaRepository turmaRepository,
   IUnityOfWork unityOfWork) : ICreateActivityUseCase
{
    private readonly IActivityRepository _activityRepository = activityRepository;
    private readonly ITurmaRepository _turmaRepository = turmaRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IUserProvider _userProvider = userProvider;

    public async Task<ResponseActivityCreatedDTO> Execute(Guid classId, RequestCreateActivityDTO request)
    {
        var teacherIdentifier = _userProvider.GetUserIdentifier()
         ?? throw new UnauthorizedException();

        await Validate(request);

        var turma = await _turmaRepository.GetByIdAsync(classId)
            ?? throw new NotFoundException("Turma não encontrada.");

        if (turma.TeacherId != teacherIdentifier)
            throw new ForbiddenException("Você não tem permissão para criar atividades para esta turma.");

        var activity = Activity.Create(request.Title, request.Type, classId, request.Instructions);

        if (activity.Type == EActivityType.QUESTION_AND_ANSWER)
            foreach (var questionDto in request.Questions!)
            {
                var alternatives = questionDto.Alternatives
                .Select(alt => (alt.Text, alt.IsCorrect))
                .ToList();

                activity.AddQuestion(questionDto.Statement, alternatives);
            }

        await _activityRepository.AddAsync(activity);
        await _unityOfWork.Commit();

        return new ResponseActivityCreatedDTO
        {
            Id = activity.Id,
            ClassId = activity.TurmaId,
            Title = activity.Title,
            Instructions = activity.Instructions,
            Type = activity.Type,
            CreatedAt = activity.CreatedAt,
            Questions = activity.Questions.Select(q => new ResponseQuestionDTO(q.Id, q.Statement, q.Order)).ToList()
        };
    }

    private static async Task Validate(RequestCreateActivityDTO request)
    {
        var validationResult = await new CreateActivityValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
