namespace MaximumSum
{
    using System;

    public class MaxSumWriter : IMaxSumWriter
    {
        private readonly IOutput _output;

        private readonly IGraph _graph;

        public MaxSumWriter(IOutput output, IGraph graph)
        {
            this._output = output;
            this._graph = graph;
        }

        public void WriteMaxSum()
        {
            this._output.Write(_graph.MaximumSum.ToString());
            this._output.Write(_graph.MaximumSumPath);
        }
    }
}