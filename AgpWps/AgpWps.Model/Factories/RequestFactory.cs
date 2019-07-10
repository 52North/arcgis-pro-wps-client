using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using System;
using Wps.Client.Models;
using Wps.Client.Models.Execution;

namespace AgpWps.Model.Factories
{
    public class RequestFactory : IRequestFactory
    {

        private readonly IFileReader _fileReader;

        public RequestFactory(IFileReader fileReader)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        public DataInput CreateDataInput(DataInputViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var dataInput = new DataInput
            {
                Identifier = viewModel.ProcessName
            };

            if (viewModel.IsReference)
            {
                dataInput.Reference = new ResourceReference
                {
                    Href = viewModel.ReferenceUrl,
                    // Schema = ???
                };
            }
            else
            {
                if (viewModel is LiteralInputViewModel lvm)
                {
                    dataInput.Data = lvm.Value;
                }
                else if (viewModel is BoundingBoxInputViewModel bvm)
                {
                    dataInput.Data = new BoundingBoxValue
                    {
                        DimensionCount = 2,
                        LowerCornerPoints = new[]
                            {bvm.RectangleViewModel.LeftBottom.Item1, bvm.RectangleViewModel.LeftBottom.Item2},
                        UpperCornerPoints = new[]
                            {bvm.RectangleViewModel.RightTop.Item1, bvm.RectangleViewModel.RightTop.Item2},
                    };
                }
                else if (viewModel is ComplexDataViewModel cvm)
                {
                    if (cvm.IsFile)
                    {
                        var inputFileData = _fileReader.Read(cvm.FilePath);
                        dataInput.Data = inputFileData;
                    }
                    else
                    {
                        dataInput.Data = cvm.Input;
                    }
                }
            }

            return !viewModel.IsReference && dataInput.Data == null ? null : dataInput;
        }

        public DataOutput CreateDataOutput(DataOutputViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var output = new DataOutput
            {
                MimeType = viewModel.SelectedFormat,
                Transmission = TransmissionMode.Value,
                Identifier = viewModel.Identifier
            };

            return output;
        }

    }
}
