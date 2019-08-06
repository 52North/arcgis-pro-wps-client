using AgpWps.Model.ViewModels;
using System;
using System.Collections.Generic;

namespace AgpWps.Model.Messages
{
    public class ExecutionFinishedMessage
    {
        public ExecutionFinishedMessage(string jobId, List<ResultItemViewModel> outputs, DateTime expiresOn)
        {
            JobId = jobId;
            Outputs = outputs;
            ExpiresOn = expiresOn;
        }

        public string JobId { get; }

        public DateTime ExpiresOn { get; }

        /// <summary>
        /// List of outputs containing their id and then their saving path, in this order.
        /// </summary>
        public List<ResultItemViewModel> Outputs { get; }

    }
}
