namespace MaximumSum.Core
{
    public class Node
    {
        public Node Left { get; set; }

        public Node Right { get; set; }

        public int? Weight { get; set; }

        public bool IsEven => Weight.HasValue && Weight.Value % 2 == 0;
    }
}
