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
    }
}
