using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace StateMachine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DoTaskAAsync();
        }

        public static Task DoTaskAAsync()
        {
            var stateMachine = new DoTaskAStateMachine();
            stateMachine.builder = AsyncTaskMethodBuilder.Create();
            stateMachine.state = -1;
            stateMachine.builder.Start(ref stateMachine);
            return stateMachine.builder.Task;
        }

        private sealed class DoTaskAStateMachine : IAsyncStateMachine
        {
            public int state;

            public AsyncTaskMethodBuilder builder;

            private TaskAwaiter _awaiter;

            private void MoveNext()
            {
                int num = state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        Console.WriteLine("Before await");
                        awaiter = Task.Delay(60000).GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            num = (state = 0);
                            _awaiter = awaiter;
                            DoTaskAStateMachine stateMachine = this;

                            // awaiter.OnCompleted(stateMachine.MoveNext);  // Just for showing the internal mechanism. In actual StateMechine, below line will be used 

                            builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = _awaiter;
                        _awaiter = default(TaskAwaiter);
                        num = (state = -1);
                    }
                    awaiter.GetResult();
                    Console.WriteLine("After await");
                }
                catch (Exception exception)
                {
                    state = -2;
                    builder.SetException(exception);
                    return;
                }
                state = -2;
                builder.SetResult();
            }

            void IAsyncStateMachine.MoveNext()
            {
                this.MoveNext();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }

            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.SetStateMachine(stateMachine);
            }
        }
    }
}
