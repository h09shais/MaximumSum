namespace MaximumSum
{
    using System;
    using System.Collections.Generic;

    public class Graph: IGraph
    {
        private readonly int[,] _collection;

        private readonly Stack<int> _stack;

        private int CollectionSize => _collection.GetLength(0);

        public Graph(int[,] collection)
        {
            this._collection = collection;
            this._stack = new Stack<int>();
        }

        public int MaximumSum => CalculatedSum();

        private int CalculatedSum()
        {
            for (var i = CollectionSize - 2; i >= 0; i--)
            {
                for (var j = 0; j <= i; j++)
                {
                    var seed = new Node {Weight = _collection[i, j]};
                    var child1 = new Node {Weight = _collection[i + 1, j]};
                    var child2 = new Node {Weight = _collection[i + 1, j + 1]};

                    var maxWeight = GetMaxWeight(seed, child1, child2);
                    if (maxWeight == null)
                    {
                        continue;
                    }

                    var weight = maxWeight.Value;
                    _stack.Push(weight);
                    seed.Weight += weight;
                    _collection[i, j] = seed.Weight;
                }
            }

            return _collection[0, 0];
        }

        public string MaximumSumPath => CalculatedPath();

        private string CalculatedPath()
        {
            // TODO: Path finding
            var result = string.Empty;
            return result;
        }

        private int? GetMaxWeight(Node seed, Node child1, Node child2)
        {
            // TODO: Even - Odd choices
            return Math.Max(child1.Weight, child2.Weight);
        }
    }
}
