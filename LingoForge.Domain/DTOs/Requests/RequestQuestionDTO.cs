namespace LingoForge.Domain.DTOs.Requests;

public record RequestQuestionDTO(string Statement, List<RequestAlternativeDTO> Alternatives);