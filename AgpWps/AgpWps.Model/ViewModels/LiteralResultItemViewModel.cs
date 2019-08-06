namespace AgpWps.Model.ViewModels
{
    public class LiteralResultItemViewModel : ResultItemViewModel
    {

        private string _value;
        public string Value
        {
            get => _value;
            set => Set(ref _value, value);
        }

    }
}
