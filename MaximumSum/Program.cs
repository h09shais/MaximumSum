namespace MaximumSum
{
    using Autofac;
    using Core;

    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new WriterModule());
            Container = builder.Build();

            WriteMaxSum();
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
