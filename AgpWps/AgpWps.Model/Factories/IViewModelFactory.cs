using AgpWps.Model.ViewModels;
using Wps.Client.Models;

namespace AgpWps.Model.Factories
{
    /// <summary>
    /// Factory used to create ViewModels
    /// </summary>
    public interface IViewModelFactory
    {

        ProcessOfferingViewModel CreateProcessOfferingViewModel(string wpsUri, ProcessSummary summary);
        DataInputViewModel CreateDataInputViewModel(Input input);
        DataOutputViewModel CreateDataOutputViewModel(Output output);
        ExecutionBuilderViewModel CreateExecutionBuilderViewModel(string wpsUri, string processId);
        ServerViewModel CreateServerViewModel(string serverUrl);

    }
}
