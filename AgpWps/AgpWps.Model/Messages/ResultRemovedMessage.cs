using System;

namespace AgpWps.Model.Messages
{
    public class ResultRemovedMessage
    {
        public ResultRemovedMessage(string jobId)
        {
            JobId = jobId ?? throw new ArgumentNullException(nameof(jobId));
        }

        public string JobId { get; }

    }
}
