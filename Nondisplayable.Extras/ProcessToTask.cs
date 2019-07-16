using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Nondisplayable.Extras
{
    /// <summary>
    /// Extension methods for using an external process as an async task.
    /// </summary>
    public static class ProcessToTask
    {
        public static async Task<int> WaitForExitAsync(this Process process, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<int>();

            void OnProcessExited(object sender, EventArgs e)
            {
                int exitCode = 0;
                if(sender is Process)
                {
                    exitCode = (sender as Process).ExitCode;
                }
                
                Task.Run(() => tcs.TrySetResult(exitCode));
            }

            process.EnableRaisingEvents = true;
            process.Exited += OnProcessExited;

            try
            {
                if(process.HasExited)
                {
                    return 0;
                }

                using(cancellationToken.Register(() => Task.Run(() => tcs.TrySetCanceled())))
                {
                    return await tcs.Task;
                }
            }
            finally
            {
                process.Exited -= OnProcessExited;
            }
        }
    }
}
