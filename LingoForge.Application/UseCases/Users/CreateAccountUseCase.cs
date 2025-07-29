using LingoForge.Application.UseCases.Auth;
using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Entities;
using LingoForge.Domain.Exceptions;
using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Interfaces.UseCases.Users;
using LingoForge.Domain.Security.Cryptography;
using LingoForge.Domain.Services;

namespace LingoForge.Application.UseCases.Users;

public class CreateAccountUseCase(
    IUserRepository userRepository,
    IUserProvider userProvider,
    IPasswordEncryption passwordEncryption,
    IUnityOfWork unityOfWork) : ICreateAccountUseCase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IPasswordEncryption _passwordEncryption = passwordEncryption;
    private readonly IUserProvider _userProvider = userProvider;

    public async Task<ResponseCreatedUserDTO> Execute(RequestCreateAccountDTO request)
    {
        _ = _userProvider.GetUserIdentifier()
          ?? throw new UnauthorizedException();

        await Validate(request);

        request.Password = _passwordEncryption.Encrypt(request.Password);

        var user = User.Create(request.Name, request.Email, request.Password);

        await _userRepository.AddAsync(user);
        await _unityOfWork.Commit();

        return new ResponseCreatedUserDTO
        {
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
        };
    }

    private async Task Validate(RequestCreateAccountDTO request)
    {
        var validationResult = await new CreateAccountValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        var emailExists = await _userRepository.ExistsByEmailAsync(request.Email);
        if (emailExists)
            throw new ConflictException("O e-mail informado já está em uso.");

        var nameExists = await _userRepository.ExistsByNameAsync(request.Name);
        if (nameExists)
            throw new ConflictException("O nome de usuário informado já está em uso.");
    }
}
