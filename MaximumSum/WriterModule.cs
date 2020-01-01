namespace MaximumSum
{
    using Autofac;
    using Core;

    public class WriterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<MaxSumWriter>().As<IMaxSumWriter>();
            builder.RegisterType<Graph>().As<IGraph>();
        }
    }
}
