namespace MaximumSum.Test
{
    using FakeItEasy;
    using Xunit;
    using FluentAssertions;

    public class MaxSumTest
    {
        public static TheoryData<int[,]> Data1
        {
            get
            {
                var data = new [,]
                {
                    {1, 0, 0, 0},
                    {8, 9, 0, 0},
                    {1, 5, 9, 0},
                    {4, 5, 2, 3},
                };

                return new TheoryData<int[,]> {data};
            }
        }

        public static TheoryData<int[,]> Data2
        {
            get
            {
                var data = new[,]
                {
                    {215, 0, 0, 0},
                    {192, 124, 0, 0},
                    {117, 269, 442, 0},
                    {218, 836, 347, 235},
                };

                return new TheoryData<int[,]> { data };
            }
        }

        public static TheoryData<int[,]> Data3
        {
            get
            {
                var data = new[,]
                {
                    {215, 0, 0, 0, 0, 0},
                    {192, 125, 0, 0, 0, 0},
                    {117, 269, 442, 0, 0, 0},
                    {218, 836, 347, 235, 0, 0},
                    {320, 805, 522, 417, 345, 0},
                    {229, 601, 728, 835, 133, 124}
                };

                return new TheoryData<int[,]> { data };
            }
        }

        [Theory]
        [MemberData(nameof(Data1))]
        public void ShouldGetMaximumSumSixteen(int[,] collection)
        {
            var graph = A.Fake<Graph>(src => src.WithArgumentsForConstructor(() => new Graph(collection)));
            graph.MaximumSum.Should().Be(16);
        }
    }
}
