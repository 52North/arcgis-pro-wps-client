using AgpWps.Model.Enums;
using AgpWps.Model.Services;
using GalaSoft.MvvmLight.Command;
using System;

namespace AgpWps.Model.ViewModels
{
    public class BoundingBoxInputViewModel : DataInputViewModel
    {
        private readonly IMapService _mapService;
        private readonly IContext _context;
        private readonly IDialogService _dialogService;

        private bool _isSelecting;
        public bool IsSelecting
        {
            get => _isSelecting;
            set
            {
                Set(ref _isSelecting, value);
                _context.Invoke(SelectZoneCommand.RaiseCanExecuteChanged);
            }
        }

        private RelayCommand _selectZoneCommand;
        public RelayCommand SelectZoneCommand
        {
            get => _selectZoneCommand;
            set => Set(ref _selectZoneCommand, value);
        }

        private RectangleViewModel _rectangleViewModel;
        public RectangleViewModel RectangleViewModel
        {
            get => _rectangleViewModel;
            set => Set(ref _rectangleViewModel, value);
        }

        public BoundingBoxInputViewModel(IMapService mapService, IContext context, IDialogService dialogService)
        {
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            SelectZoneCommand = new RelayCommand(SelectZone, () => !IsSelecting);
        }

        private void SelectZone()
        {
            if (!IsSelecting)
            {
                try
                {
                    _mapService.SelectZone((r) =>
                    {
                        if (r != null)
                        {
                            RectangleViewModel = new RectangleViewModel(r.LeftBottom, r.RightTop);
                        }
                        else
                        {
                            IsSelecting = false;
                        }
                    });
                    IsSelecting = true;
                }
                catch (InvalidOperationException e)
                {
                    _dialogService.ShowMessageDialog("Invalid Operation", e.Message, DialogMessageType.Error);
                }
            }
        }
    }
}
