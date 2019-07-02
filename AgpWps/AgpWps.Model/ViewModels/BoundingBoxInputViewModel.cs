using AgpWps.Model.Services;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace AgpWps.Model.ViewModels
{
    public class BoundingBoxInputViewModel : DataInputViewModel
    {
        private readonly IMapService _mapService;

        private ICommand _selectZoneCommand;
        public ICommand SelectZoneCommand
        {
            get => _selectZoneCommand;
            set => Set(ref _selectZoneCommand, value);
        }

        public BoundingBoxInputViewModel(IMapService mapService)
        {
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            SelectZoneCommand = new RelayCommand(SelectZone);
        }

        private void SelectZone()
        {
            _mapService.TriggerZoneSelection();
        }
    }
}
