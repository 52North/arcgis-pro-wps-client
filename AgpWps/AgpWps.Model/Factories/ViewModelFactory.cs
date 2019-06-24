using AgpWps.Model.ViewModels;
using Wps.Client.Models;

namespace AgpWps.Model.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        public ProcessOfferingViewModel CreateProcessOfferingViewModel(ProcessSummary sum)
        {
            return new ProcessOfferingViewModel
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
