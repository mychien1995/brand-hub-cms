using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandHub.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            new CoreDataImporter().ImportProvince("VN");
            Console.ReadLine();
        }
    }
}
