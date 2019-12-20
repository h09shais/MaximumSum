namespace MaximumSum
{
    using System;
    class Program
    {
        public Helper Helper { get; } = new Helper();
        static void Main(string[] args)
        {
            new Program().GetMaximumSum();
        }

        public void GetMaximumSum()
        {
            var grid = Helper.ReadInput("../../Matrix.txt");
            var gridSize = grid.GetLength(0);

            for (var i = gridSize - 2; i >= 0; i--)
            {
                for (var j = 0; j <= i; j++)
                {
                    var seed = new Node { Weight = grid[i, j] };
                    var child1 = new Node { Weight = grid[i + 1, j] };
                    var child2 = new Node { Weight = grid[i + 1, j + 1] };

                    if (child1.IsEven && child2.IsEven && seed.IsEven)
                    {
                        grid[i, j] = 0;
                    }

                    else if (!child1.IsEven && !child2.IsEven && !seed.IsEven)
                    {
                        grid[i, j] = 0;
                    }
                    else
                    {
                        var maxWeight = getMaxWeight(seed, child1, child2);
                        seed.Weight += maxWeight;
                        grid[i, j] = seed.Weight;
                    }

                    PrintGrid(grid);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Max sum: {0}", grid[0, 0]);
            Console.WriteLine("Path:");
        }

        private int getMaxWeight(Node seed, Node child1, Node child2)
        {
            if (child1.IsEven && child2.IsEven)
            {
                return Math.Max(child1.Weight, child2.Weight);
            }

            if (!child1.IsEven && !child2.IsEven)
            {
                return Math.Max(child1.Weight, child2.Weight);
            }

            return child1.IsEven && !seed.IsEven || child2.Weight == 0 ? child1.Weight: child2.Weight;
        }

        private void PrintGrid(int[,] grid)
        {
            var gridSize = grid.GetLength(0);

            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
