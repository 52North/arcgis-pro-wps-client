using AgpWps.Model.ViewModels;
using System.Collections.Generic;
using Wps.Client.Models.Execution;
using Wps.Client.Models.Requests;

namespace AgpWps.Model.Factories
{
    public interface IRequestFactory
    {

        /// <summary>
        /// Create an execution request for the WPS standard.
        /// </summary>
        /// <param name="dataInputViewModels">A list of data input view models.</param>
        /// <param name="dataOutputViewModels">A list of data outputs view models.</param>
        /// <returns>The execution request</returns>
        ExecuteRequest CreateExecuteRequest(string processId, List<DataInputViewModel> dataInputViewModels, List<DataOutputViewModel> dataOutputViewModels);

        /// <summary>
        /// Create a data input from <see cref="DataInputViewModel"/>.
        /// </summary>
        /// <param name="viewModel">The viewmodel</param>
        /// <returns>The data input</returns>
        DataInput CreateDataInput(DataInputViewModel viewModel);

        /// <summary>
        /// Create a data output from <see cref="DataOutputViewModel"/>.
        /// </summary>
        /// <param name="viewModel">The viewmodel</param>
        /// <returns>The data output</returns>
        DataOutput CreateDataOutput(DataOutputViewModel viewModel);

    }
}
