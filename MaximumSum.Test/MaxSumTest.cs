namespace MaximumSum.Test
{
    using FakeItEasy;
    using Xunit;
    using Core;
    using FluentAssertions;

    public class MaxSumTest
    {
        public static TheoryData<string[]> Data1
        {
            get
            {
                var data = new []
                {
                    "1",
                    "8 9",
                    "1 5 9",
                    "4 5 2 3",
                };

                return new TheoryData<string[]> {data};
            }
        }

        public static TheoryData<string[]> Data2
        {
            get
            {
                var data = new[]
                {
                    "99",
                    "12 67",
                    "33 45 66",
                    "23 56 91 74",
                    "56 83 12 56 77",
                    "56 88 23 45 71 34",
                    "45 87 23 56 88 11 45"
                };

                return new TheoryData<string[]> { data };
            }
        }

        [Theory]
        [MemberData(nameof(Data1))]
        public void ShouldGetMaximumSumSixteen(string[] data)
        {
            var repository = A.Fake<IGraphRepository>();
            A.CallTo(() => repository.GetGraph()).Returns(data);

            var reader = A.Fake<DataReader>(src => src.WithArgumentsForConstructor(() => new DataReader(repository)));
            var graph = A.Fake<Graph>(src => src.WithArgumentsForConstructor(() => new Graph(reader)));

            graph.MaxSum.Should().Be(16);
        }

        [Theory]
        [MemberData(nameof(Data2))]
        public void ShouldGetMaximumSumFourHundredSeventy(string[] data)
        {
            var repository = A.Fake<IGraphRepository>();
            A.CallTo(() => repository.GetGraph()).Returns(data);

            var reader = A.Fake<DataReader>(src => src.WithArgumentsForConstructor(() => new DataReader(repository)));
            var graph = A.Fake<Graph>(src => src.WithArgumentsForConstructor(() => new Graph(reader)));

            graph.MaxSum.Should().Be(470);
        }
    }
}
