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

        private readonly IMapService _mapService;
        private readonly IDialogService _dialogService;
        private readonly IWpsClient _wpsClient;
        private readonly IContext _context;
        private readonly IRequestFactory _requestFactory;

        public ViewModelFactory(IMapService mapService, IDialogService dialogService, IWpsClient wpsClient, IContext context, IRequestFactory requestFactory)
        {
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
        }

        public ProcessOfferingViewModel CreateProcessOfferingViewModel(string wpsUri, ProcessSummary sum)
        {
            return new ProcessOfferingViewModel(wpsUri, sum.Identifier, _dialogService, _wpsClient, _context, this)
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
                vm = new BoundingBoxInputViewModel(_mapService, _context, _dialogService);
            }
            else if (input.Data is ComplexData cd)
            {
                vm = new ComplexDataViewModel(_dialogService);
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

        public DataOutputViewModel CreateDataOutputViewModel(Output output)
        {
            if (output == null) throw new ArgumentNullException(nameof(output));

            var formats = output.Data.Formats.Select(f => f.MimeType).ToArray();

            var vm = new DataOutputViewModel(_dialogService)
            {
                Formats = new ObservableCollection<string>(formats),
                Identifier = output.Identifier
            };

            return vm;
        }

        public ExecutionBuilderViewModel CreateExecutionBuilderViewModel(string wpsUri, string processId)
        {
            return new ExecutionBuilderViewModel(wpsUri, processId, _wpsClient, _context, this, _requestFactory, _dialogService);
        }
    }
}
