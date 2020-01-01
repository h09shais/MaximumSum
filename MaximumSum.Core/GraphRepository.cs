namespace MaximumSum.Core
{
    public class GraphRepository: IGraphRepository
    {
        private string[] Graph { get; }

        public GraphRepository()
        {
            Graph = Helper.ReadData("../../Matrix.txt");
        } 

        public string[] GetGraph()
        {
            return Graph;
        }
    }
}
