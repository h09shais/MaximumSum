namespace MaximumSum
{
    using System;

    class Program
    {
        public Helper Helper { get; } = new Helper();

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
                
                Console.WriteLine();
                Console.WriteLine("Max sum: {0}", graph.GetMaximumSum());
                Console.WriteLine("Path: {0}", graph.GetMaximumSumPath());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
