using Microsoft.EntityFrameworkCore;
using System;

namespace Matrix.AppSample.Infrastructure.EFCore
{
    public interface IEFDbContextAccessor<T> : IDisposable where T : DbContext
    {
        void Register(T context);
        T Get();
        void Clear();
    }
}
