namespace MaximumSum
{
    using System;
    using System.IO;
    public class Helper
    {
        public int[,] ReadInput(string filename)
        {
            using var reader = new StreamReader(filename);
            var row = 0;
            var length = int.Parse(reader.ReadLine() ?? throw new InvalidOperationException("Bad data!"));

            var collection = new int[length, length];

            while (!reader.EndOfStream)
            {
                var tokens = reader.ReadLine()?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens == null) continue;

                for (var i = 0; i < tokens.Length; i++)
                {
                    collection[row, i] = int.Parse(tokens[i]);
                }

                row++;
            }

            return collection;
        }

        public void Display(int[,] collection)
        {
            var collectionSize = collection.GetLength(0);

            for (var i = 0; i < collectionSize; i++)
            {
                for (var j = 0; j < collectionSize; j++)
                {
                    Console.Write(collection[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

