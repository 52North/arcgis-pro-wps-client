namespace AgpWps.Model.ViewModels
{
    public class LiteralDataOutputViewModel : DataOutputViewModel
    {

        private bool _isIncluded;
        public bool IsIncluded
        {
            get => _isIncluded;
            set => Set(ref _isIncluded, value);
        }

    }
}
