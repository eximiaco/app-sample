using System;

namespace Matrix.AppSample.Infrastructure.EFCore
{
    public sealed class MatrixAppDBContextAccessor : IEFDbContextAccessor<MatrixAppDBContext>
    {
        private MatrixAppDBContext _context;
        private bool _disposed = false;

        public MatrixAppDBContext Get()
        {
            return _context ?? throw new InvalidOperationException("Contexto deve ser registrado!");
        }

        public void Register(MatrixAppDBContext context)
        {
            _disposed = false;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Clear()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _context?.Dispose();
            _context = null;
            _disposed = true;
        }
    }
}
