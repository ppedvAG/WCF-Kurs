using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKaffeeService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var client = new ServiceReference1.Service1Client();
            var result = client.GetKaffeeListe();

            foreach (var k in result)
            {
                Console.WriteLine($"{k.Hersteller} {k.Sorte} {(k.GanzeBohnen ? "🥚" : "🥡")}");
            }


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
