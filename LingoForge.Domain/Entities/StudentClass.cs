namespace LingoForge.Domain.Entities;

public class StudentClass : BaseEntity
{
    public Guid TurmaId { get; private set; }
    public Guid StudentId { get; private set; }
    public DateTime EnrolledAt { get; private set; } = DateTime.UtcNow;

    private StudentClass(Guid turmaId, Guid studentId)
    {
        TurmaId = turmaId;
        StudentId = studentId;
    }

    public static StudentClass Create(Guid turmaId, Guid studentId)
    {
        return new StudentClass(turmaId, studentId);
    }
}
