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
                var rootValue = int.TryParse(graph[0], out var value) ? (int?)value : null;

                var root = new Node { Weight = rootValue };
                BuildGraph(root, 0, graph, 0);
                return root;
            }
        }

        private void BuildGraph(Node node, int index, string[] rows, int rowIndex)
        {
            if (rows.Length == rowIndex + 1) return;

            var rowItems = RowItems(rows, rowIndex);

            if (rowItems.All(x => !x.HasValue))
            {
                throw new Exception("Invalid data");
            }

            if (rowItems[index] != null)
            {
                node.Left = new Node { Weight = rowItems[index] };

                BuildGraph(node.Left, index, rows, rowIndex + 1);
            }

            if (rowItems[index + 1] != null)
            {
                node.Right = new Node { Weight = rowItems[index + 1] };

                BuildGraph(node.Right, index + 1, rows, rowIndex + 1);
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
