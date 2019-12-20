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
            var length = int.Parse(reader.ReadLine());

            var matrix = new int[length, length];

            while (!reader.EndOfStream)
            {
                var tokens = reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < tokens.Length; i++)
                    matrix[row, i] = int.Parse(tokens[i]);

                row++;
            }

            return matrix;
        }
    }
}

