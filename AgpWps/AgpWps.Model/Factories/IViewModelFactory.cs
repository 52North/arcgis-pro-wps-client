using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using Wps.Client.Models;
using Wps.Client.Services;

namespace AgpWps.Model.Factories
{
    public interface IViewModelFactory
    {

        ProcessOfferingViewModel CreateProcessOfferingViewModel(ProcessSummary summary, IDialogService dialogService, IWpsClient wpsClient);

    }
}
