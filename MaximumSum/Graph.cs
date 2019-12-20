namespace MaximumSum
{
    using System;

    public class Graph: IGraph
    {
        private readonly int[,] _collection;

        private int CollectionSize => _collection.GetLength(0);

        public Graph(int[,] collection)
        {
            this._collection = collection;
        }

        public int MaximumSum
        {
            get
            {
                for (var i = CollectionSize - 2; i >= 0; i--)
                {
                    for (var j = 0; j <= i; j++)
                    {
                        var seed = new Node {Weight = _collection[i, j]};
                        var child1 = new Node {Weight = _collection[i + 1, j]};
                        var child2 = new Node {Weight = _collection[i + 1, j + 1]};

                        if (child1.IsEven && child2.IsEven && seed.IsEven)
                        {
                            _collection[i, j] = 0;
                        }

                        else if (!child1.IsEven && !child2.IsEven && !seed.IsEven)
                        {
                            _collection[i, j] = 0;
                        }
                        else
                        {
                            var maxWeight = getMaxWeight(seed, child1, child2);
                            seed.Weight += maxWeight;
                            _collection[i, j] = seed.Weight;
                        }
                    }
                }

                return _collection[0, 0];
            }
        }

        public string MaximumSumPath => string.Empty;
        

        private int getMaxWeight(Node seed, Node child1, Node child2)
        {
            if (child1.IsEven && child2.IsEven || !child1.IsEven && !child2.IsEven)
            {
                return Math.Max(child1.Weight, child2.Weight);
            }

            return child1.IsEven && !seed.IsEven || child2.Weight == 0 ? child1.Weight : child2.Weight;
        }
    }
}
