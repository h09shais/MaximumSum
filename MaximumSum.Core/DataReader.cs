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

        public Node Root
        {
            get
            {
                var rows = _repository.GetGraph();
                var rootValue = NullableTryParseInt(rows[0]);

                var root = new Node { Weight = (int)rootValue };
                BuildGraph(root, 0, rows, 0);
                return root;
            }
        }

        private int? NullableTryParseInt(string token)
        {
            return int.TryParse(token, out var value) ? (int?)value : null;
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
    }
}
