namespace MaximumSum.Core
{
    public class Node
    {
        public Node Left { get; set; }

        public Node Right { get; set; }

        public int Weight { get; set; }

        public bool IsEven => Weight % 2 == 0;
    }
}
