using System;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace Hangfire.Attributes
{
    public class UseQueueFromParameterAttribute : JobFilterAttribute, IElectStateFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueAttribute"/> class
        /// using the specified queue name.
        /// </summary>
        /// <param name="queue">Queue name.</param>
        public UseQueueFromParameterAttribute(int parameterIndex)
        {
            ParameterIndex = parameterIndex;
        }

        public int ParameterIndex { get; private set; }

        public void OnStateElection(ElectStateContext context)
        {
            var enqueuedState = context.CandidateState as EnqueuedState;
            if (enqueuedState != null)
            {
                enqueuedState.Queue = (context.BackgroundJob.Job.Args[ParameterIndex] as string).Replace("\"", string.Empty);
            }
        }
    }

    public class ProlongSuccessJobExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
    {
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(35);
        }

        public void OnStateUnapplied(ApplyStateContext content, IWriteOnlyTransaction transcation) { }
    }
}
