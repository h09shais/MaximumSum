namespace MaximumSum.Core
{
    public interface IDataReader
    {
        Node Root { get; set; }

        DataReader BuildGraph(IGraphRepository repository);
    }
}
