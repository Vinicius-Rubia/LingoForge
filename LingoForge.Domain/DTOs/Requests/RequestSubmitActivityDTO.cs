namespace LingoForge.Domain.DTOs.Requests;

public class RequestSubmitActivityDTO
{
    // Usado para atividades de Escrita Livre
    public string? FreeTextContent { get; set; }

    // Usado para atividades de Perguntas e Respostas
    public List<RequestAnswerItemDTO>? Answers { get; set; }
}