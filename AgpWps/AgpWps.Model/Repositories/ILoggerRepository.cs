using System;
using Wps.Client.Models.Requests;

namespace AgpWps.Model.Repositories
{
    public interface ILoggerRepository
    {

        /// <summary>
        /// Log an error to a file in the repository.
        /// </summary>
        /// <param name="jobId">The id of the job that caused the error.</param>
        /// <param name="request">The request used to launch the job.</param>
        /// <param name="ex">The exception that has been raised during the execution.</param>
        /// <returns>The path to the log file.</returns>
        string LogExceptionReport(string jobId, ExecuteRequest request, Exception ex);

    }
}
