using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public DataInputViewModel CreateDataInputViewModel(Input input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var formats = input.Data.Formats.Select(f => f.MimeType).ToArray();
            var isOptional = input.MinimumOccurrences == 0;

            DataInputViewModel vm;

            if (input.Data is LiteralData ld)
            {
                vm = new LiteralInputViewModel();
            }
            else if (input.Data is BoundingBoxData bbd)
            {
                vm = new BoundingBoxInputViewModel();
            }
            else
            {
                vm = new DataInputViewModel();
            }

            vm.IsOptional = isOptional;
            vm.ProcessName = input.Identifier;
            vm.Formats = new ObservableCollection<string>(formats);

            return vm;
        }
    }
}
