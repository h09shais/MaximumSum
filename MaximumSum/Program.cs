namespace MaximumSum
{
    using System;
    using Autofac;

    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new WriterModule());
            Container = builder.Build();

            WriteDate();
            //new Program().GetMaximumSum();
        }

        private void GetMaximumSum()
        {
            try
            {
                var collection = Helper.ReadData("../../Matrix.txt");
                var graph = new Graph(collection);
                
                Console.WriteLine("Max sum: {0}", graph.MaximumSum);
                Console.WriteLine("Path: {0}", graph.MaximumSumPath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static void WriteDate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IMaxSumWriter>();
                writer.WriteMaxSum();
            }
        }
    }
}
