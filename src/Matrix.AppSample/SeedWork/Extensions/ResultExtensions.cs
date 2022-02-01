using System.Linq;
using CSharpFunctionalExtensions;

namespace System
{
    public static class ResultCombine
    {
        public static Result Combine(params Result[] resultados)
        {
            var errors = resultados
                .Where(c => c.IsFailure)
                .Select(c => c.Error)
                .ToList();

            return errors.Any()
                ? Result.Failure(errors.Aggregate((x,y) => x + ", " + y))
                : Result.Success();
        }
    }
}