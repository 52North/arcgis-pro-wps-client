using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using Wps.Client.Models;
using Wps.Client.Models.Data;
using Wps.Client.Services;

namespace AgpWps.Model.Factories
{
    public interface IViewModelFactory
    {

        ProcessOfferingViewModel CreateProcessOfferingViewModel(string wpsUri, ProcessSummary summary, IDialogService dialogService, IWpsClient wpsClient, IContext context, IViewModelFactory viewModelFactory);
        DataInputViewModel CreateDataInputViewModel(Input input);

    }
}
