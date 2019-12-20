namespace MaximumSum
{
    public class Node
    {
        public int Weight { get; set; }

        public bool IsEven => Weight % 2 == 0;
    }
}
