using AgpWps.Model.Exceptions;
using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Wps.Client.Models;
using Wps.Client.Models.Execution;
using Wps.Client.Models.Requests;

namespace AgpWps.Model.Factories
{
    public class RequestFactory : IRequestFactory
    {

        private readonly IFileReader _fileReader;

        public RequestFactory(IFileReader fileReader)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        public ExecuteRequest CreateExecuteRequest(string processId, List<DataInputViewModel> dataInputViewModels, List<DataOutputViewModel> dataOutputViewModels)
        {
            if (processId == null) throw new ArgumentNullException(nameof(processId));
            if (dataInputViewModels == null) throw new ArgumentNullException(nameof(dataInputViewModels));
            if (dataOutputViewModels == null) throw new ArgumentNullException(nameof(dataOutputViewModels));

            var validInputs = new List<DataInputViewModel>();
            foreach (var input in dataInputViewModels)
            {
                if (input.IsReference)
                {
                    if (!string.IsNullOrEmpty(input.ReferenceUrl))
                    {
                        validInputs.Add(input);
                        continue;
                    }

                    if (!input.IsOptional) throw new NullInputException(input.ProcessName);
                }
                else
                {
                    if (input is LiteralInputViewModel lvm)
                    {
                        if (lvm.Value != null)
                        {
                            validInputs.Add(input);
                            continue;
                        }

                        if (!lvm.IsOptional) throw new NullInputException(input.ProcessName);
                    }
                    else if (input is BoundingBoxInputViewModel bvm)
                    {
                        if (bvm.RectangleViewModel != null)
                        {
                            validInputs.Add(input);
                            continue;
                        }

                        if (!bvm.IsOptional) throw new NullInputException(input.ProcessName);
                    }
                    else if (input is ComplexDataViewModel cvm)
                    {
                        if (cvm.Input != null)
                        {
                            validInputs.Add(input);
                            continue;
                        }

                        if (!cvm.IsOptional) throw new NullInputException(input.ProcessName);
                    }
                }
            }

            // Select all the inputs that have a valid data or if they are reference, otherwise filter them out.
            var dataInputs = validInputs.Select(CreateDataInput);

            // Select only the outputs that have a file path, otherwise it means they are not wanted by the user.
            var dataOutputs = dataOutputViewModels.Where(o => !string.IsNullOrEmpty(o.FilePath)).Select(CreateDataOutput).ToArray();

            if (!dataOutputs.Any())
            {
                throw new NoOutputSelectedException();
            }

            var executeRequest = new ExecuteRequest
            {
                ExecutionMode = ExecutionMode.Asynchronous,
                Identifier = processId,
                Inputs = dataInputs.ToArray(),
                Outputs = dataOutputs.ToArray(),
                ResponseType = ResponseType.Document
            };

            return executeRequest;
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
