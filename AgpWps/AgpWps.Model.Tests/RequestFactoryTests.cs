using AgpWps.Model.Exceptions;
using AgpWps.Model.Factories;
using AgpWps.Model.ViewModels;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Wps.Client.Models;
using Wps.Client.Models.Execution;
using Xunit;

namespace AgpWps.Model.Tests
{
    public class RequestFactoryTests
    {

        private readonly RequestFactory _requestFactory;

        public RequestFactoryTests()
        {
            _requestFactory = new RequestFactory(new FileReaderMock());
        }

        [Fact]
        public void CreateDataInput_LiteralInputViewModelGiven_IsValue_ShouldCreateInput()
        {
            const string processName = "testProcess";
            const string value = "test value";

            var vm = new LiteralInputViewModel
            {
                IsReference = false,
                ProcessName = processName,
                Value = value
            };

            var input = _requestFactory.CreateDataInput(vm);
            input.Identifier.Should().Be(processName);
            input.Data.Should().BeOfType<LiteralDataValue>();
            if (input.Data is LiteralDataValue ldv)
            {
                ldv.Value.Should().Be(value);
            }
        }

        [Fact]
        public void CreateDataInput_BoundingBoxInputViewModelGiven_IsValue_ShouldCreateInput()
        {
            const string processName = "testProcess";
            const int expectedDimensionCount = 2;

            var vm = new BoundingBoxInputViewModel(new MapServiceMock(), new ContextMock(), new DialogServiceMock())
            {
                ProcessName = processName,
                RectangleViewModel = new RectangleViewModel(new Tuple<double, double>(1.0, 2.0),
                    new Tuple<double, double>(3.0, 4.0))
            };

            var input = _requestFactory.CreateDataInput(vm);
            input.Identifier.Should().Be(processName);
            input.Data.Should().BeOfType<BoundingBoxValue>();
            if (input.Data is BoundingBoxValue bbv)
            {
                bbv.DimensionCount.Should().Be(expectedDimensionCount);
                bbv.LowerCornerPoints.Should().BeEquivalentTo(1.0, 2.0);
                bbv.UpperCornerPoints.Should().BeEquivalentTo(3.0, 4.0);
            }
        }

        [Fact]
        public void CreateDataInput_ComplexDataInputViewModelGiven_IsValue_ShouldCreateInput()
        {
            const string processName = "testProcess";
            const string inputValue = "test value";

            var vm = new ComplexDataViewModel(new DialogServiceMock())
            {
                ProcessName = processName,
                IsFile = false,
                Input = inputValue
            };

            var input = _requestFactory.CreateDataInput(vm);
            input.Identifier.Should().Be(processName);
            input.Data.Should().Be(inputValue);
        }

        [Fact]
        public void CreateDataInput_ComplexDataInputViewModelGiven_IsFilePath_ShouldCreateInput()
        {
            const string processName = "testProcess";

            var vm = new ComplexDataViewModel(new DialogServiceMock())
            {
                ProcessName = processName,
                IsFile = true,
                FilePath = FileReaderMock.CvmTestPath
            };

            var input = _requestFactory.CreateDataInput(vm);
            input.Identifier.Should().Be(processName);
            input.Data.Should().Be(FileReaderMock.CvmTestPathResult);
        }

        [Fact]
        public void CreateDataInput_InputViewModelGiven_IsReference_ShouldCreateReferencedInput()
        {
            const string processName = "testProcess";

            var vm = new DataInputViewModel
            {
                ProcessName = processName,
                IsReference = true,
                ReferenceUrl = "ref uri"
            };

            var input = _requestFactory.CreateDataInput(vm);
            input.Identifier.Should().Be(processName);
            input.Data.Should().BeNull();
            input.Reference.Href.Should().Be("ref uri");
        }

        [Fact]
        public void CreateDataInput_NullInputGiven_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { _requestFactory.CreateDataInput(null); });
        }

        [Fact]
        public void CreateDataOutput_NullInputGiven_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { _requestFactory.CreateDataOutput(null); });
        }

        [Fact]
        public void CreateDataOutput_OutputViewModelGiven_ShouldCreateOutput()
        {
            const string processName = "processName";
            var selectedFormat = new FormatViewModel
            {
                MimeType = "test format",
                SelectedEncoding = "encoding",
                SelectedSchema = "schema"
            };

            var vm = new DataOutputViewModel()
            {
                Identifier = processName,
                SelectedFormat = selectedFormat
            };

            var output = _requestFactory.CreateDataOutput(vm);
            output.Identifier.Should().Be(processName);
            output.MimeType.Should().Be(selectedFormat.MimeType);
            output.Schema.Should().Be(selectedFormat.SelectedSchema);
            output.Encoding.Should().Be(selectedFormat.SelectedEncoding);
            output.Transmission.Should().Be(TransmissionMode.Value);
        }

        [Fact]
        public void CreateExecuteRequest_ValidInputAndOutputViewModelsGiven_ShouldCreateRequest()
        {
            const string processName = "processName";

            var inputs = new List<DataInputViewModel>
            {
                new LiteralInputViewModel {Value = "a"},
                new BoundingBoxInputViewModel(new MapServiceMock(), new ContextMock(), new DialogServiceMock())
                {
                    RectangleViewModel = new RectangleViewModel(new Tuple<double, double>(0.0, 0.0), new Tuple<double, double>(0.0, 0.0) )
                },
                new DataInputViewModel(), // Dummy input, should be removed by the factory
                new ComplexDataViewModel(new DialogServiceMock()) { Input = "i" },
                new DataInputViewModel{IsReference = true, ReferenceUrl = "ref uri"},
            };

            var selectedFormat = new FormatViewModel
            {
                MimeType = "test format",
                SelectedEncoding = "encoding",
                SelectedSchema = "schema"
            };
            var outputs = new List<DataOutputViewModel>
            {
                new LiteralDataOutputViewModel{SelectedFormat = selectedFormat, IsIncluded = true},
                new DataOutputViewModel(), // Dummy output, should be removed by the factory
            };

            var request = _requestFactory.CreateExecuteRequest(processName, inputs, outputs);
            request.Identifier.Should().Be(processName);
            request.Inputs.Should().HaveCount(4);
            request.Outputs.Should().HaveCount(1);
            request.ResponseType.Should().Be(ResponseType.Document);
            request.ExecutionMode.Should().Be(ExecutionMode.Asynchronous);
        }

        [Fact]
        public void CreateExecuteRequest_RequiredLiteralValue_NullValueGiven_ShouldThrowNullInputException()
        {
            Assert.Throws<NullInputException>(() =>
            {
                var inputs = new List<DataInputViewModel>
                {
                    new LiteralInputViewModel{IsOptional = false, Value = null}
                };

                _requestFactory.CreateExecuteRequest("", inputs,
                    new List<DataOutputViewModel>());
            });
        }

        [Fact]
        public void CreateExecuteRequest_RequiredBoundingBoxValue_NullValueGiven_ShouldThrowNullInputException()
        {
            Assert.Throws<NullInputException>(() =>
            {
                var inputs = new List<DataInputViewModel>
                {
                    new BoundingBoxInputViewModel(new MapServiceMock(), new ContextMock(), new DialogServiceMock()) { IsOptional = false, RectangleViewModel = null}
                };

                _requestFactory.CreateExecuteRequest("", inputs,
                    new List<DataOutputViewModel>());
            });
        }

        [Fact]
        public void CreateExecuteRequest_RequiredComplexValue_NullValueGiven_ShouldThrowNullInputException()
        {
            Assert.Throws<NullInputException>(() =>
            {
                var inputs = new List<DataInputViewModel>
                {
                    new ComplexDataViewModel(new DialogServiceMock()) { IsOptional = false, Input = null }
                };

                _requestFactory.CreateExecuteRequest("", inputs,
                    new List<DataOutputViewModel>());
            });
        }

        [Fact]
        public void CreateExecuteRequest_NoOutputSelected_ShouldThrowNoOutputSelectedException()
        {
            Assert.Throws<NoOutputSelectedException>(() =>
            {
                var outputs = new List<DataOutputViewModel>
                {
                    new DataOutputViewModel(),
                    new DataOutputViewModel(),
                    new DataOutputViewModel(),
                };

                _requestFactory.CreateExecuteRequest("", new List<DataInputViewModel>(),
                    outputs);
            });
        }

        [Fact]
        public void CreateExecuteRequest_RequiredReferencedData_NullReference_ShouldThrowNullInputException()
        {
            Assert.Throws<NullInputException>(() =>
            {
                var inputs = new List<DataInputViewModel>
                {
                    new DataInputViewModel { IsOptional = false, IsReference = true, ReferenceUrl = null }
                };

                _requestFactory.CreateExecuteRequest("", inputs,
                    new List<DataOutputViewModel>());
            });
        }

        [Fact]
        public void CreateExecuteRequest_NullProcessIdGiven_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _requestFactory.CreateExecuteRequest(null, new List<DataInputViewModel>(),
                    new List<DataOutputViewModel>());
            });
        }

        [Fact]
        public void CreateExecuteRequest_NullInputsGiven_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    _requestFactory.CreateExecuteRequest("", null, new List<DataOutputViewModel>());
                });
        }
        [Fact]
        public void CreateExecuteRequest_NullOutputsGiven_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    _requestFactory.CreateExecuteRequest("", new List<DataInputViewModel>(), null);
                });
        }

    }
}
