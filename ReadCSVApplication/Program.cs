using ReadCSV.Core;
using ReadCSV.Core.AdapterInterfaces;
using ReadCSVBusinessLayer;
using ReadCSVDependencyInjection;
using System;

namespace ReadCSVApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processing...");
            // Setup Dependency Injection -- No Startup.cs in console apps
            var csvReaderAdapter = DependencyInjections.Instance.ServiceProvider.GetService(typeof(ICSVReader<Row>));
            // Get Our App Service
            var readCSVService = new ReadCSVService((ICSVReader<Row>)csvReaderAdapter, "./Input/data.csv");
            // Process and write files
            readCSVService.WriteAddressesToFile("./Output/addresses_output.txt");
            readCSVService.WriteNamesToFile("./Output/names_output.txt");
            // Output sorted names to console
            Console.WriteLine("Names Output: (File saved in [project_root]/Output/names_output.txt)");
            Console.WriteLine("");
            Console.Write(System.IO.File.ReadAllText("./Output/names_output.txt"));
            // Output sorted addresses to console
            Console.WriteLine("Addresses Output: (File saved in [project_root]/Output/addresses_output.txt)");
            Console.WriteLine("");
            Console.Write(System.IO.File.ReadAllText("./Output/addresses_output.txt"));
            // Done
            Console.WriteLine("Press any key to exit;");
            Console.ReadKey();
        }
    }
}
