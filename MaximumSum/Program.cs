namespace MaximumSum
{
    using System;
    using Autofac;
    using Core;

    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            try
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule(new WriterModule());
                Container = builder.Build();

                WriteMaxSum();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void WriteMaxSum()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IMaxSumWriter>();
                writer.WriteMaxSum();
            }
        }
    }
}
