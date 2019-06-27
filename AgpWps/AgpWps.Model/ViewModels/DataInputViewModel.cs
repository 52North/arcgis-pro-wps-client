using GalaSoft.MvvmLight;

namespace AgpWps.Model.ViewModels
{
    public class DataInputViewModel : ViewModelBase
    {

        private bool _isReference;
        public bool IsReference
        {
            get => _isReference;
            set => Set(ref _isReference, value);
        }

    }
}
