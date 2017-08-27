using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    class Functions
    {
        public IEnumerable<Country> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(Path.ChangeExtension(fileName, ".csv"));

            return lines.Select(line =>
            {
                string[] data = line.Split(',');
                return new Country(data[0], data[1], data[2], data[3], Int32.Parse(data[4]), Int32.Parse(data[5]), data[6], data[7]);
            }).Where(x => x.Name != "Country");
        }
    }
}
