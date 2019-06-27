using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using Wps.Client.Models;
using Wps.Client.Services;

namespace AgpWps.Model.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        public ProcessOfferingViewModel CreateProcessOfferingViewModel(ProcessSummary sum, IDialogService dialogService, IWpsClient wpsClient)
        {
            return new ProcessOfferingViewModel(sum.Identifier, dialogService, wpsClient)
            {
                ProcessName = sum.Identifier,
                TransmissionModes = sum.OutputTransmission.ToString(),
                JobControlOptions = sum.JobControlOptions,
                Keywords = sum.Keywords != null ? string.Join(", ", sum.Keywords) : null,
                Model = sum.ProcessModel,
                Version = sum.ProcessVersion,
                Abstract = sum.Abstract,
            };
        }
    }
}
