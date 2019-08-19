using AgpWps.Model.ViewModels;
using System;
using System.Collections.Generic;

namespace AgpWps.Model.Messages
{
    public class ExecutionFinishedMessage
    {
        public ExecutionFinishedMessage(string jobId, List<ResultItemViewModel> outputs, DateTime expiresOn, TimeSpan elapsedTime)
        {
            JobId = jobId;
            Outputs = outputs;
            ExpiresOn = expiresOn;
            ElapsedTime = elapsedTime;
        }

        public string JobId { get; }

        public DateTime ExpiresOn { get; }
        public TimeSpan ElapsedTime { get; }

        /// <summary>
        /// List of outputs containing their id and then their saving path, in this order.
        /// </summary>
        public List<ResultItemViewModel> Outputs { get; }

    }
}
