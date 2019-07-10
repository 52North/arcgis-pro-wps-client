using AgpWps.Model.Factories;
using AgpWps.Model.ViewModels;
using FluentAssertions;
using System;
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
            input.Data.Should().Be(value);
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

    }
}
