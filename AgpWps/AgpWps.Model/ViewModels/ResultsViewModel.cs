using AgpWps.Model.Messages;
using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace AgpWps.Model.ViewModels
{
    public class ResultsViewModel : ViewModelBase
    {
        private readonly IContext _context;

        private ObservableCollection<ResultViewModel> _results = new ObservableCollection<ResultViewModel>();
        public ObservableCollection<ResultViewModel> Results
        {
            get => _results;
            set => Set(ref _results, value);
        }

        public ResultsViewModel(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            MessengerInstance.Register<ExecutionFinishedMessage>(this, (msg) =>
            {
                _context.Invoke(() =>
                {
                    Results.Add(new ResultViewModel
                    {
                        JobId = msg.JobId,
                        Processes = new ObservableCollection<ResultViewModel>(msg.Outputs)
                    });
                });
            });
        }

    }
}
