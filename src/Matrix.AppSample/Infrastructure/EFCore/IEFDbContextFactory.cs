using Microsoft.EntityFrameworkCore;

namespace Matrix.AppSample.Infrastructure.EFCore
{
    public interface IEFDbContextFactory<T> where T : DbContext
    {
        T Create();
        T CreateWithTransaction();
    }
}
