using AgpWps.Model.ViewModels;
using Wps.Client.Models;

namespace AgpWps.Model.Factories
{
    public interface IViewModelFactory
    {

        ProcessOfferingViewModel CreateProcessOfferingViewModel(string wpsUri, ProcessSummary summary);
        DataInputViewModel CreateDataInputViewModel(Input input);
        DataOutputViewModel CreateDataOutputViewModel(Output output);

    }
}
