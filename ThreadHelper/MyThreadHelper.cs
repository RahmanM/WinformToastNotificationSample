using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToastNotifications
{

    public class MyThreadHelper<T>
    {

        public void Execute(Func<T> action, Action<T> onCompletion)
        {
            var task = Task.Run(() => action());
            task.GetAwaiter().OnCompleted(() =>
            {
                onCompletion(task.Result);
            });
        }

        public async Task<T> Execute(Func<T> action)
        {
            var task = await Task.Run(() => action());
            return task;
        }

        public void Execute(Func<T> action, Action<T> onCompletion, Action<Exception> onException)
        {
            var task = Task.Run(() => action());
            task.GetAwaiter().OnCompleted(() =>
            {
                try
                {
                    onCompletion(task.Result);
                }
                catch (AggregateException aggregated)
                {
                    var builder = new StringBuilder();
                    foreach (var element in aggregated.InnerExceptions)
                    {
                        builder.AppendLine(element.Message);
                    }
                    var ex = new Exception(builder.ToString(), aggregated);
                    onException(ex);
                }
            });
        }

        public void Execute(Func<T> action, Action<T> onCompletion, Action<Exception> onException, CancellationToken cancel)
        {
            var task = Task.Run(() => action());
            task.GetAwaiter().OnCompleted(() =>
            {
                try
                {
                    cancel.ThrowIfCancellationRequested();
                    onCompletion(task.Result);
                }
                catch (AggregateException aggregated)
                {
                    var builder = new StringBuilder();
                    foreach (var element in aggregated.InnerExceptions)
                    {
                        builder.AppendLine(element.Message);
                    }
                    var ex = new Exception(builder.ToString(), aggregated);
                    onException(ex);
                }
                catch (OperationCanceledException cancelException)
                {
                    var ex = new Exception("Operation was cancelled by user!", cancelException);
                    onException(ex);
                }
            });
        }

        public void Execute(Func<T> action, Action<T> onCompletion, Action<Exception> onException, CancellationToken token, IProgress<int> progress)
        {
            var task = Task.Run(() => action());
            var tasks = new[] { task };
            var counter = 1;
            Task progressTask = Task.Run(async () =>
            {
                try
                {
                    while (!Task.WhenAll(tasks).IsCompleted)
                    {
                        progress.Report(counter);
                        await Task.Delay(10);
                        counter++;

                        if (task.IsCompleted)
                        {
                            onCompletion(task.Result);
                        }

                        token.ThrowIfCancellationRequested();
                    }
                }
                catch (AggregateException aggregated)
                {
                    var builder = new StringBuilder();
                    foreach (var element in aggregated.InnerExceptions)
                    {
                        builder.AppendLine(element.Message);
                    }
                    var ex = new Exception(builder.ToString(), aggregated);
                    onException(ex);
                }
                catch (OperationCanceledException cancelException)
                {
                    var ex = new Exception("Operation was cancelled by user!", cancelException);
                    onException(ex);
                }

            });

        }
    }
    }
