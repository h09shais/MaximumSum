namespace MaximumSum.Core
{
    using System.Linq;
    using System.Collections.Generic;

    public class Graph : IGraph
    {
        private readonly IDataReader _reader;

        private readonly ICollection<Node> _path;

        private readonly IEnumerable<int> _maxPath;

        public Graph(IDataReader reader)
        {
            this._reader = reader;
            this._path = new List<Node>();
            this._maxPath = new List<int>();
        }

        private IEnumerable<int> Paths => MaxSumPathCalculator(_reader.Root, _path, _maxPath);

        private IEnumerable<int> MaxSumPathCalculator(Node node, ICollection<Node> path, IEnumerable<int> maxPath)
        {
            if (!path.Any())
            {
                _path.Add(node);
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
                _path.Add(node.Left);
                maxPath = MaxSumPathCalculator(node.Left, path, maxPath);
                _path.Remove(node.Left);
            }

            if (node.Right != null && node.IsEven != node.Right.IsEven)
            {
                _path.Add(node.Right);
                maxPath = MaxSumPathCalculator(node.Right, path, maxPath);
                _path.Remove(node.Right);
            }

            return maxPath;
        }

        public int MaximumSum => Paths.Sum();

        public string MaximumSumPath => string.Join(", ", Paths);
    }
}
