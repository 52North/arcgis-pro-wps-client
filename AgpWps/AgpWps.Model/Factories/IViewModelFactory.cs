using AgpWps.Model.ViewModels;
using Wps.Client.Models;

namespace AgpWps.Model.Factories
{
    public interface IViewModelFactory
    {

        ProcessOfferingViewModel CreateProcessOfferingViewModel(ProcessSummary summary);

    }
}
