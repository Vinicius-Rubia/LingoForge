namespace LingoForge.Domain.DTOs.Responses;

public class ResponseSubmissionDTO
{
    public Guid SubmissionId { get; set; }
    public Guid ActivityId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime SubmittedAt { get; set; }
}