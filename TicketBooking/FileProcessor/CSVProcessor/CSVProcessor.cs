using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.FileProcessor.CSVProcessor;

public class CSVProcessor : IFileLoader
{
    public static List<string[]> Load(string path)
    {
        using var reader = new StreamReader(path);

        // Read header line, no processing required
        reader.ReadLine();

        List<string[]> result = new();
        while (!reader.EndOfStream)
        {
            result.Add(reader.ReadLine().Split(','));
        }
        return result;
    }

}
