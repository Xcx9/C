using System;
using System.Threading;
using System.Threading.Tasks;

namespace lab5
{
    public static class RetryPolicy
    {
        public static async Task<T> ExecuteWithRetry<T>(
            Func<Task<T>> operation,
            CancellationToken cancellationToken = default)
        {
            Exception lastException = null;
            for (int attempt = 0; attempt < Config.RetryMaxAttempts; attempt++)
            {
                try
                {
                    return await operation().ConfigureAwait(false);
                }
                catch (Exception ex) when (!(ex is OperationCanceledException))
                {
                    lastException = ex;
                    if (attempt == Config.RetryMaxAttempts - 1)
                        break;

                    await Task.Delay(Config.RetryBackoff[attempt], cancellationToken)
                              .ConfigureAwait(false);
                }
            }
            throw new InvalidOperationException(
                $"Operation failed after {Config.RetryMaxAttempts} attempts.", lastException);
        }
    }
}
