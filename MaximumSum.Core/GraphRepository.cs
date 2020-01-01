namespace MaximumSum.Core
{
    public class GraphRepository: IGraphRepository
    {
        private string[] Graph { get; }

        public GraphRepository()
        {
            Graph = Helper.ReadData("../../Graph.txt");
        } 

        public string[] GetGraph()
        {
            return Graph;
        }
    }
}
