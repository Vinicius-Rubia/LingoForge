namespace LingoForge.Domain.DTOs.Responses;

public class BaseResponseErrorDTO
{
    public List<string> ErrorMessages { get; set; }

    public BaseResponseErrorDTO(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public BaseResponseErrorDTO(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
