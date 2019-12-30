namespace MaximumSum
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Graph : IGraph
    {
        private readonly int[,] _collection;

        private readonly int[] _flattenCollection;

        private readonly Stack<Node> _stack;

        private int CollectionSize => _collection.GetLength(0);

        private readonly SimpleGraph _graph;

        public Graph(string[] rows)
        {
            this._graph = BuildGraph(rows);
        }

        private SimpleGraph BuildGraph(string[] rows)
        {
            if (!rows.Any()) return new SimpleGraph();

            var rootValue = NullableTryParseInt(rows[0]);

            if (rootValue == null) return new SimpleGraph();

            var root = new Node { Weight = (int)rootValue };

            try
            {
                BuildGraph(root, 0, rows, 0);
                return new SimpleGraph { Root = root };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new SimpleGraph();
            }
        }

        private void BuildGraph(Node parentNode, int parentNodeIndex, IReadOnlyList<string> rows, int parentRowIndex)
        {
            if (rows.Count == parentRowIndex + 1) return;

            var numbersInRows = rows[parentRowIndex + 1]
                .Trim().Split(' ')
                .Select(NullableTryParseInt)
                .ToArray();

            if (numbersInRows.All(x => !x.HasValue))
            {
                throw new Exception("A row contains only null/non integers elements");
            }

            if (numbersInRows[parentNodeIndex] != null)
            {
                parentNode.Left = new Node { Weight = (int)numbersInRows[parentNodeIndex] };

                BuildGraph(parentNode.Left, parentNodeIndex, rows, parentRowIndex + 1);
            }

            if (numbersInRows[parentNodeIndex + 1] != null)
            {
                parentNode.Right = new Node { Weight = (int)numbersInRows[parentNodeIndex + 1] };

                BuildGraph(parentNode.Right, parentNodeIndex + 1, rows, parentRowIndex + 1);
            }
        }

        private int? NullableTryParseInt(string token)
        {
            return int.TryParse(token, out var value) ? (int?)value : null;
        }

        private IEnumerable<int> Paths => MaxSumPathCalculator(_graph.Root, new List<Node>(), new List<int>());

        private IEnumerable<int> MaxSumPathCalculator(Node node, ICollection<Node> path, IEnumerable<int> maxPath)
        {
            if (!path.Any())
            {
                path.Add(node);
            }

            if (node.Left == null && node.Right == null)
            {
                var newPath = path.Select(x => x.Weight).ToList();

                maxPath = maxPath.ToList();
                if (newPath.Sum() > maxPath.Sum())
                {
                    maxPath = newPath;
                }

                return maxPath;
            }

            if (node.Left != null && node.IsEven != node.Left.IsEven)
            {
                path.Add(node.Left);
                maxPath = MaxSumPathCalculator(node.Left, path, maxPath);
                path.Remove(node.Left);
            }

            if (node.Right != null && node.IsEven != node.Right.IsEven)
            {
                path.Add(node.Right);
                maxPath = MaxSumPathCalculator(node.Right, path, maxPath);
                path.Remove(node.Right);
            }

            return maxPath;
        }

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

        public void Build(Node root)
        {
            for (var i = CollectionSize - 2; i >= 0; i--)
            {
                for (var j = 0; j <= i; j++)
                {
                    var seed = new Node { Weight = _collection[i, j], XCoordinate = i, YCoordinate = j };
                    var child1 = new Node { Weight = _collection[i + 1, j], XCoordinate = i + 1, YCoordinate = j };
                    var child2 = new Node { Weight = _collection[i + 1, j + 1], XCoordinate = i + 1, YCoordinate = j + 1 };


                }
            }
        }

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

        public int MaximumSum => Paths.Sum();
        
        public string MaximumSumPath => string.Join(", ", Paths);
    }
}
