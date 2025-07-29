using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Exceptions;
using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Interfaces.UseCases.Auth;
using LingoForge.Domain.Security.Cryptography;
using LingoForge.Domain.Security.Tokens;

namespace LingoForge.Application.UseCases.Auth;

public class LoginUseCase(
    IUserRepository userRepository,
    IPasswordEncryption passwordEncryption,
    IJwtTokenGenerator jwtTokenGenerator) : ILoginUseCase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IPasswordEncryption _passwordEncryption = passwordEncryption;

    public async Task<ResponseAuthenticatedUserDTO> Execute(RequestLoginDTO request)
    {
        await Validate(request);

        var user = await _userRepository.GetByEmailOrUsernameAsync(request.LoginIdentifier)
            ?? throw new InvalidLoginException();

        var passwordMatch = _passwordEncryption.Verify(request.Password, user.Password);

        if (!passwordMatch)
            throw new InvalidLoginException();

        var jwt = _jwtTokenGenerator.Generate(user);

        return new ResponseAuthenticatedUserDTO
        {
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
            Token = jwt,
        };
    }

    private static async Task Validate(RequestLoginDTO request)
    {
        var validationResult = await new LoginValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
