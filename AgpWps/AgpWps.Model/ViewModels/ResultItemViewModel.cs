using GalaSoft.MvvmLight;

namespace AgpWps.Model.ViewModels
{
    public class ResultItemViewModel : ViewModelBase
    {

        private string _resultId;
        public string ResultId
        {
            get => _resultId;
            set => Set(ref _resultId, value);
        }

    }
}
