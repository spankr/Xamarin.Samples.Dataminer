using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Sample.Dataminer.Logic;

namespace Xamarin.Sample.Dataminer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new SampleProjectService();

            var projects = service.GetSampleProjects(@"C:\Users\Veldrane\Source\Xamarin\xamarin-forms-samples");

            int i = 1;
            foreach (var p in projects.Where(p => p.Metadata != null).OrderBy(p=>p.Metadata.Level))
            {
                System.Console.WriteLine($"{i++:D3}: {p.Metadata.Level} {p.Name}");
            }

            System.Console.ReadKey();
        }
    }
}
