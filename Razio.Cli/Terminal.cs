using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Razio.Cli
{
    public sealed class Terminal : TaskScheduler
    {
        private readonly LinkedList<Task> _tasks = new();

        private readonly LinkedList<TaskCompletionSource<int>> _kbhitPromises = new();

        public void Run()
        {

        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            bool locked = false;
            try
            {
                Monitor.TryEnter(_tasks, ref locked);
                if (locked)
                {
                    return _tasks;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            finally
            {
                if (locked)
                {
                    Monitor.Exit(_tasks);
                }
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (!taskWasPreviouslyQueued || TryDequeue(task))
            {
                return TryExecuteTask(task);
            }
            else
            {
                return false;
            }
        }

        protected override void QueueTask(Task task)
        {
            lock (_tasks)
            {
                _tasks.AddLast(task);
            }
        }

        protected override bool TryDequeue(Task task)
        {
            lock (_tasks)
            {
                return _tasks.Remove(task);
            }
        }
    }
}
