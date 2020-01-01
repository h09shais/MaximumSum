namespace MaximumSum.Core
{
    using System.Linq;
    using System.Collections.Generic;

    public class Graph : IGraph
    {
        private readonly IDataReader _graph;
        private readonly IGraphRepository _repository;

        public Graph(IDataReader graph, IGraphRepository repository)
        {
            this._repository = repository;
            this._graph = graph.BuildGraph(repository);
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
