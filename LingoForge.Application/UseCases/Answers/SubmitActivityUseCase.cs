using LingoForge.Application.UseCases.Students;
using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Entities;
using LingoForge.Domain.Enums;
using LingoForge.Domain.Exceptions;
using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Interfaces.UseCases.Answers;
using LingoForge.Domain.Services;

namespace LingoForge.Application.UseCases.Answers;

public class SubmitActivityUseCase(
    IActivityRepository activityRepository,
    ITurmaRepository turmaRepository,
    IAnswerRepository answerRepository,
    IUserProvider userProvider,
    IUnityOfWork unityOfWork) : ISubmitActivityUseCase
{
    private readonly IActivityRepository _activityRepository = activityRepository;
    private readonly ITurmaRepository _turmaRepository = turmaRepository;
    private readonly IAnswerRepository _answerRepository = answerRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IUserProvider _userProvider = userProvider;

    public async Task<ResponseSubmissionDTO> Execute(Guid activityId, RequestSubmitActivityDTO request)
    {
        var studentIdentifier = _userProvider.GetUserIdentifier()
         ?? throw new UnauthorizedException();

        await Validate(request);

        var atividade = await _activityRepository.GetByIdWithDetailsAsync(activityId)
            ?? throw new NotFoundException("Atividade não encontrada.");

        if (!await _turmaRepository.IsStudentEnrolledInClassAsync(studentIdentifier, atividade.TurmaId))
            throw new ForbiddenException("Você não tem permissão para responder a esta atividade, pois não está na turma.");

        if (await _answerRepository.ExistsByActivityAndStudentAsync(activityId, studentIdentifier))
            throw new ConflictException("Você já enviou uma resposta para esta atividade.");

        Answer answers;

        if (atividade.Type == EActivityType.FREE_WRITING)
        {
            if (string.IsNullOrWhiteSpace(request.FreeTextContent))
                throw new BadRequestException("O conteúdo da escrita é obrigatório para este tipo de atividade.");

            answers = Answer.Create(activityId, studentIdentifier, request.FreeTextContent);
        }
        else
        {
            if (request.Answers is null || !request.Answers.Any())
                throw new BadRequestException("A lista de respostas é obrigatória para este tipo de atividade.");

            // Valida se o número de respostas enviadas bate com o número de questões
            if (request.Answers.Count != atividade.Questions.Count)
                throw new BadRequestException("Você deve responder a todas as questões da atividade.");

            answers = Answer.CreateWithQuestionAndAnswers(activityId, studentIdentifier, []);

            foreach (var answerDto in request.Answers)
            {
                var questao = atividade.Questions.FirstOrDefault(q => q.Id == answerDto.QuestionId)
                    ?? throw new BadRequestException($"A questão com ID '{answerDto.QuestionId}' não pertence a esta atividade.");

                // O método de domínio valida se a alternativa pertence à questão
                answers.AddChoice(questao, answerDto.ChosenAlternativeId);
            }
        }

        await _answerRepository.AddAsync(answers);
        await _unityOfWork.Commit();

        return new ResponseSubmissionDTO
        {
            SubmissionId = answers.Id,
            ActivityId = answers.ActivityId,
            StudentId = answers.StudentId,
            SubmittedAt = answers.CreatedAt
        };
    }

    private async static Task Validate(RequestSubmitActivityDTO request)
    {
        var validationResult = await new SubmitActivityValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
