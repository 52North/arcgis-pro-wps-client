using AgpWps.Model.ViewModels;
using Wps.Client.Models.Execution;

namespace AgpWps.Model.Factories
{
    public interface IRequestFactory
    {

        /// <summary>
        /// Create a data input from <see cref="DataInputViewModel"/>.
        /// </summary>
        /// <param name="viewModel">The viewmodel</param>
        /// <returns>The data input</returns>
        DataInput CreateDataInput(DataInputViewModel viewModel);

    }
}
