using AgpWps.Model.Services;
using GalaSoft.MvvmLight.Command;
using System;

namespace AgpWps.Model.ViewModels
{
    public class BoundingBoxInputViewModel : DataInputViewModel
    {
        private readonly IMapService _mapService;
        private readonly IContext _context;

        private bool _isSelecting;

        public bool IsSelecting
        {
            get => _isSelecting;
            set
            {
                _context.Invoke(SelectZoneCommand.RaiseCanExecuteChanged);
                Set(ref _isSelecting, value);
            }
        }

        private RelayCommand _selectZoneCommand;
        public RelayCommand SelectZoneCommand
        {
            get => _selectZoneCommand;
            set => Set(ref _selectZoneCommand, value);
        }

        public BoundingBoxInputViewModel(IMapService mapService, IContext context)
        {
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _context = context ?? throw new ArgumentNullException(nameof(context));

            SelectZoneCommand = new RelayCommand(SelectZone, () => !IsSelecting);
        }

        private void SelectZone()
        {
            IsSelecting = true;
            _mapService.SelectZone((r) =>
            {
                IsSelecting = false;
            });
        }
    }
}
