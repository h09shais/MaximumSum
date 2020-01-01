namespace MaximumSum.Core
{
    using System;
    using System.Collections.Generic;
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
                var rootValue = NullableTryParseInt(graph[0]);

                var root = new Node { Weight = (int)rootValue };
                BuildGraph(root, 0, graph, 0);
                return root;
            }
        }

        private int? NullableTryParseInt(string token)
        {
            return int.TryParse(token, out var value) ? (int?)value : null;
        }

        private void BuildGraph(Node node, int index, IReadOnlyList<string> rows, int rowIndex)
        {
            if (rows.Count == rowIndex + 1) return;

            var numbersInRows = rows[rowIndex + 1]
                .Trim().Split(' ')
                .Select(NullableTryParseInt)
                .ToArray();

            if (numbersInRows.All(x => !x.HasValue))
            {
                throw new Exception("A row contains only null/non integers elements");
            }

            if (numbersInRows[index] != null)
            {
                node.Left = new Node { Weight = (int)numbersInRows[index] };

                BuildGraph(node.Left, index, rows, rowIndex + 1);
            }

            if (numbersInRows[index + 1] != null)
            {
                node.Right = new Node { Weight = (int)numbersInRows[index + 1] };

                BuildGraph(node.Right, index + 1, rows, rowIndex + 1);
            }
        }
    }
}
