namespace MaximumSum
{
    using System;
    using System.IO;
    public class Helper
    {
        public static int[,] ReadInput(string fileName)
        {
            var length = 0;
            using (var reader = new StreamReader(fileName))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    length++;
                    line = reader.ReadLine();
                }
            }

            var collection = new int[length, length];

            using (var reader = new StreamReader(fileName))
            {
                var row = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < tokens.Length; i++)
                    {
                        collection[row, i] = int.Parse(tokens[i]);
                    }
                    
                    row++;
                }
            }

            return collection;
        }

        public static void Display(int[,] collection)
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

