using Microsoft.Extensions.DependencyInjection;
using ReadCSV.Core;
using ReadCSV.Core.AdapterInterfaces;
using ReadCSVDAL;
using System;

namespace ReadCSVDependencyInjection
{
    /// <summary>
    /// A Singleton to give us access to DI
    /// </summary>
    public sealed class DependencyInjections
    {
        private static readonly Lazy<DependencyInjections> lazy =
            new Lazy<DependencyInjections>(() => new DependencyInjections());

        /// <value>
        /// Gets the ServiceProvider if you retrieved a reference to through Instance first
        /// </value>
        public readonly ServiceProvider ServiceProvider;

        /// <summary>
        /// Gets a reference to the dependecy resolver
        /// </summary>
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
