namespace MaximumSum
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            new Program().GetMaximumSum();
        }

        private void GetMaximumSum()
        {
            try
            {
                var collection = Helper.ReadInput("../../Matrix.txt");
                var graph = new Graph(collection);
                
                Console.WriteLine("Max sum: {0}", graph.MaximumSum);
                Console.WriteLine("Path: {0}", graph.MaximumSumPath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
