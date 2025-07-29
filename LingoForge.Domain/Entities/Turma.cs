using LingoForge.Domain.Enums;
using LingoForge.Domain.Exceptions;

namespace LingoForge.Domain.Entities;

public class Turma : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid TeacherId { get; private set; }

    private readonly List<StudentClass> _enrollments = [];
    public IReadOnlyCollection<StudentClass> Enrollments => _enrollments.AsReadOnly();

    private Turma(string name, Guid teacherId, string? description)
    {
        Name = name;
        TeacherId = teacherId;
        Description = description;

        Validate();
    }

    public static Turma Create(string name, Guid teacherId, string? description)
    {
        return new Turma(name, teacherId, description);
    }

    public void AddStudent(User student)
    {
        // Regras de negócio
        if (student.Role is not EUserRole.STUDENT)
            throw new DomainException("Apenas alunos podem ser adicionados a uma turma.");

        if (_enrollments.Any(e => e.StudentId == student.Id))
            return; // Aluno já está na turma

        _enrollments.Add(StudentClass.Create(Id, student.Id));
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveStudent(Guid studentId)
    {
        var enrollment = _enrollments.FirstOrDefault(e => e.StudentId == studentId);
        if (enrollment is not null)
        {
            _enrollments.Remove(enrollment);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new DomainException("Nome da turma é obrigatório.");

        int nameLength = Name.Trim().Length;

        if (nameLength < 6)
            throw new DomainException("Nome da turma deve ter no mínimo 6 caracteres.");

        if (nameLength > 20)
            throw new DomainException("Nome da turma deve ter no máximo 20 caracteres.");
    }
}
