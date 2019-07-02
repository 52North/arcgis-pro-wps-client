using AgpWps.Model.Services;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace AgpWps.Model.ViewModels
{
    public class BoundingBoxInputViewModel : DataInputViewModel
    {

        private ICommand _selectZoneCommand;
        public ICommand SelectZoneCommand
        {
            get => _selectZoneCommand;
            set => Set(ref _selectZoneCommand, value);
        }

        public BoundingBoxInputViewModel()
        {
            SelectZoneCommand = new RelayCommand(SelectZone);
        }

        private void SelectZone()
        {
        }
    }
}
