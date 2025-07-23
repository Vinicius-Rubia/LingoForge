namespace LingoForge.Domain.Interfaces.Repositories;

public interface IUnityOfWork
{
    Task Commit();
}
