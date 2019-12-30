namespace MaximumSum
{
    using System;
    using System.Collections.Generic;

    public class Graph : IGraph
    {
        private readonly int[,] _collection;

        private readonly int[] _flattenCollection;

        private readonly Stack<Node> _stack;

        private int CollectionSize => _collection.GetLength(0);

        public Graph(int[,] collection)
        {
            this._collection = collection;
            this._flattenCollection = RowMajorOrderFlatten(collection);
            this._stack = new Stack<Node>();
        }

        private int[] RowMajorOrderFlatten(int[,] collection)
        {
            var k = 0;
            var flattenCollection = new int[CollectionSize * CollectionSize];
            for (var i = 0; i < CollectionSize; i++)
            {
                for (var j = 0; j < CollectionSize; j++)
                {
                    flattenCollection[k++] = collection[i, j];
                }
            }

            return flattenCollection;
        }
        
        public int MaximumSum => CalculatedSum();

        private int CalculatedSum()
        {
            for (var i = CollectionSize - 2; i >= 0; i--)
            {
                for (var j = 0; j <= i; j++)
                {
                    var seed = new Node {Weight = _collection[i, j], XCoordinate = i, YCoordinate = j};
                    var child1 = new Node {Weight = _collection[i + 1, j], XCoordinate = i + 1, YCoordinate = j};
                    var child2 = new Node {Weight = _collection[i + 1, j + 1], XCoordinate = i + 1, YCoordinate = j + 1};

                    var node = GetMaxWeight(seed, child1, child2);
                    seed.Weight += node.Weight;
                    _stack.Push(node);
                    _collection[i, j] = seed.Weight;
                }
            }

            //_stack.Push(new Node { XCoordinate = 0, YCoordinate = 0 });
            var root = _collection[0, 0];
            return root;
        }

        public string MaximumSumPath => CalculatedPath();

        private void AddPath(Node node)
        {
            var index = GetIndex(node);
            var item = GetItem(index);
            //_stack.Push(item);
        }

        private int GetItem(int index)
        {
            var item = _flattenCollection[index];
            return item;
        }

        private int GetIndex(Node node)
        {
            var index = node.XCoordinate * CollectionSize + node.YCoordinate;
            return index;
        }

        private string CalculatedPath()
        {
            var items = new List<int>();
            foreach (var item in _stack)
            {
                var index = GetIndex(item);
                var element = GetItem(index);
                items.Add(element);
            }

            //for (var i = 0; i < CollectionSize - 1; i++)
            //{
            //    for (var j = 0; j < i; j++)
            //    {

            //    }
            //}

            var result = string.Join(", ", items);
            return result;
        }

        private Node GetMaxWeight(Node seed, Node child1, Node child2)
        {
            // TODO: Even - Odd choices
            return Math.Max(child1.Weight, child2.Weight) == child1.Weight? child1: child2;
        }
    }
}
