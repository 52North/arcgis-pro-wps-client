using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using Wps.Client.Models;
using Wps.Client.Models.Data;
using Wps.Client.Services;

namespace AgpWps.Model.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        public ProcessOfferingViewModel CreateProcessOfferingViewModel(string wpsUri, ProcessSummary sum, IDialogService dialogService, IWpsClient wpsClient, IContext context, IViewModelFactory viewModelFactory)
        {
            return new ProcessOfferingViewModel(wpsUri, sum.Identifier, dialogService, wpsClient, context, viewModelFactory)
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

        public DataInputViewModel CreateDataInputViewModel(Data data)
        {
            switch (data)
            {
                case LiteralData ld:
                    return new LiteralInputViewModel
                    {
                        Value = "test"
                    };
                default:
                    return null;
            }
        }
    }
}
