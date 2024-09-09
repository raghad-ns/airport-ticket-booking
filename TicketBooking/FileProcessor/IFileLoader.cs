using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.FileProcessor;

public interface IFileLoader
{
    public static abstract List<string[]> Load(string path);
}
