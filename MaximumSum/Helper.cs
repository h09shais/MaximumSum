namespace MaximumSum
{
    using System;
    using System.Linq;
    using System.IO;
    public class Helper
    {
        public static string[] ReadData(string fileName)
        {
            string fileContent;
            using (var reader = new StreamReader(fileName))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }
    }
}

