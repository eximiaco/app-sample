namespace Matrix.AppSample.SeedWork
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
