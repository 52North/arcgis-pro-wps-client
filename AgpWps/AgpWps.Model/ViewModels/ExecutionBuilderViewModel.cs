using System;
using GalaSoft.MvvmLight;
using Wps.Client.Services;

namespace AgpWps.Model.ViewModels
{
    public class ExecutionBuilderViewModel : ViewModelBase
    {

        private readonly string _processId;
        private readonly IWpsClient _wpsClient;

        public ExecutionBuilderViewModel(string processId, IWpsClient wpsClient)
        {
            _processId = processId ?? throw new ArgumentNullException(nameof(processId));
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));


        }

    }
}
