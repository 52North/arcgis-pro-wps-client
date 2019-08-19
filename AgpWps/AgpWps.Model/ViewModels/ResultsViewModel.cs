using AgpWps.Model.Messages;
using AgpWps.Model.Repositories;
using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AgpWps.Model.ViewModels
{
    public class ResultsViewModel : ViewModelBase
    {
        private readonly IContext _context;
        private readonly IResultRepository _resultRepo;

        private ObservableCollection<ResultViewModel> _results = new ObservableCollection<ResultViewModel>();
        public ObservableCollection<ResultViewModel> Results
        {
            get => _results;
            set => Set(ref _results, value);
        }

        public ResultsViewModel(IContext context, IResultRepository resultRepo)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _resultRepo = resultRepo ?? throw new ArgumentNullException(nameof(resultRepo));

            Results = new ObservableCollection<ResultViewModel>(_resultRepo.GetResults());

            MessengerInstance.Register<ExecutionFinishedMessage>(this, (msg) =>
            {
                _context.Invoke(() =>
                {
                    var result = new ResultViewModel
                    {
                        JobId = msg.JobId,
                        Processes = new ObservableCollection<ResultItemViewModel>(msg.Outputs),
                        ElapsedTime = msg.ElapsedTime
                    };

                    _resultRepo.AddResult(result);
                    Results.Add(result);
                });
            });

            MessengerInstance.Register<ResultRemovedMessage>(this, (msg) =>
            {
                var result = Results.FirstOrDefault(r =>
                    r.JobId.Equals(msg.JobId, StringComparison.InvariantCultureIgnoreCase));
                if (result != null)
                {
                    Results.Remove(result);
                    _resultRepo.RemoveResult(result);
                }
            });
        }

    }
}
