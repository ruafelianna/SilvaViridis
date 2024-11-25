using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Common.Concurrent
{
    public class AsyncMutex
    {
        protected AsyncMutex(
            Task mutexTask,
            Task taskToAwait,
            ManualResetEventSlim releaseEvent
        )
        {
            TaskToAwait = taskToAwait;
            _mutexTask = mutexTask;
            _releaseEvent = releaseEvent;
        }

        public Task TaskToAwait { get; }

        public static AsyncMutex Aquire(
            string mutexName,
            CancellationToken cancellationToken
        )
        {
            var completionSource = new TaskCompletionSource<Unit>();
            var releaseEvent = new ManualResetEventSlim();

            var mutexTask = Task.Factory.StartNew(
                state => {
                    try
                    {
                        using var mutex = new Mutex(false, mutexName);
                        try
                        {
                            mutex.WaitOne();
                        }
                        catch (AbandonedMutexException)
                        {
                        }

                        completionSource.SetResult(Unit.Default);

                        releaseEvent.Wait();

                        mutex.ReleaseMutex();
                    }
                    catch (Exception ex)
                    {
                        completionSource.TrySetException(ex);
                    }
                },
                state: null,
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
            );

            return new(
                mutexTask,
                completionSource.Task,
                releaseEvent
            );
        }

        public async Task ReleaseAsync()
        {
            _releaseEvent.Set();

            if (_mutexTask is not null)
            {
                await _mutexTask;
            }
        }

        private readonly Task _mutexTask;
        private readonly ManualResetEventSlim _releaseEvent;
    }
}
