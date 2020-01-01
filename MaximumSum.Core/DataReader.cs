namespace MaximumSum.Core
{
    using System;
    using System.Linq;

    public class DataReader: IDataReader
    {
        private readonly IGraphRepository _repository;

        public DataReader(IGraphRepository repository)
        {
            this._repository = repository;
        }

        public Node GraphElement
        {
            get
            {
                var graph = _repository.GetGraph();
                var weight = int.TryParse(graph[0], out var value) ? (int?)value : null;

                var root = new Node { Weight = weight };
                BuildGraph(root, graph, 0, 0);
                return root;
            }
        }

        private void BuildGraph(Node node, string[] rows, int leftIndex, int rightIndex)
        {
            if (rows.Length == leftIndex + 1) return;

            var rowItems = RowItems(rows, leftIndex);

            if (rowItems.All(x => !x.HasValue))
            {
                throw new Exception("Invalid data");
            }

            if (rowItems[rightIndex] != null)
            {
                node.Left = new Node { Weight = rowItems[rightIndex] };

                BuildGraph(node.Left, rows, leftIndex + 1, rightIndex);
            }

            if (rowItems[rightIndex + 1] != null)
            {
                node.Right = new Node { Weight = rowItems[rightIndex + 1] };

                BuildGraph(node.Right, rows, leftIndex + 1, rightIndex + 1);
            }
        }

        private int?[] RowItems(string[] rows, int rowIndex)
        {
            return rows[rowIndex + 1]
                .Trim().Split(' ')
                .Select(item => int.TryParse(item, out var value) ? (int?)value : null)
                .ToArray();
        }
    }
}
