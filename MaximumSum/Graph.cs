namespace MaximumSum
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Core;

    public class Graph : IGraph
    {
        private readonly SimpleGraph _graph;

        public Graph()
        {
            var collection = Helper.ReadData("../../Matrix.txt");
            this._graph = BuildGraph(collection);
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

        public int MaximumSum => Paths.Sum();
        
        public string MaximumSumPath => string.Join(", ", Paths);
    }
}
