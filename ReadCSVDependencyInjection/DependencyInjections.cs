using Microsoft.Extensions.DependencyInjection;
using ReadCSV.Core;
using ReadCSV.Core.AdapterInterfaces;
using ReadCSVDAL;
using System;

namespace ReadCSVDependencyInjection
{
    // This class is implemented as a singleton
    public sealed class DependencyInjections
    {
        private static readonly Lazy<DependencyInjections> lazy =
            new Lazy<DependencyInjections>(() => new DependencyInjections());

        // You only access the ServiceProvider if you retrieved a reference to through Instance first
        public  readonly ServiceProvider ServiceProvider;

        // Use this to get a reference to the dependecy resolver
        public static DependencyInjections Instance { get { return lazy.Value; } }

        private DependencyInjections()
        {
            //setup our DI
            ServiceProvider = new ServiceCollection()
                .AddSingleton(typeof(ICSVReader<Row>), typeof(CSVReaderAdapter))
                .AddSingleton(typeof(ICSVSchema<Row>), typeof(CSVSchema<Row>))
                .BuildServiceProvider();
        }
    }
}
