namespace MaximumSum
{
    public class Node
    {
        public int Weight { get; set; }

        public int XCoordinate { get; set; }

        public int YCoordinate { get; set; }
        
        public bool IsEven => Weight % 2 == 0;
    }
}
