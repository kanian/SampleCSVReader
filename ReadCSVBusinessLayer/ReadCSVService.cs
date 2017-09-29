using ReadCSV.Core;
using ReadCSV.Core.AdapterInterfaces;
using ReadCSV.ServiceInterfaces;
using ReadCSVDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadCSVBusinessLayer
{
    /// <summary>
    /// A service that contain our application logic to read csv files, and write output files
    /// </summary>
    public class ReadCSVService : IReadCSVService<Row>
    {
        private readonly ICSVReader<Row> _csvReaderAdapter;
        private readonly string _fileName;
        /// <value>
        /// Gets the rows read from a CSV files
        /// </value>
        public IEnumerable<Row> Rows { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="csvReaderAdapter">
        /// Adapter to access a CSV file
        /// </param>
        /// <param name="fileName">
        /// path to the CSV Fileto access
        /// </param>
        public ReadCSVService(ICSVReader<Row> csvReaderAdapter, string fileName)
        {
            _csvReaderAdapter = csvReaderAdapter;
            _fileName = fileName;
            var content = _csvReaderAdapter.ReadCSVFile(_fileName);
            Rows = (IEnumerable<Row>)_csvReaderAdapter.GetRows( _csvReaderAdapter.GetCSVFileDescriptor(content));
        }

        /// <summary>
        /// Sorts addresses from the currently loaded CSV file
        /// </summary>
        /// <returns>
        /// A list of alphabetically sorted addresses from the currently loaded CSV file
        /// </returns>
        public List<string> SortAddresses()
        {
            var aggregate = new List<string>();
            if (Rows == default(Dictionary<string, int>))
                return aggregate;
            var addresses = Rows.Select(x => x.Address).ToList();
            var explodedAddresses = addresses
                .Select(x => x.Split(new char[0]))
                .OrderBy(x => x[1])
                .Select(x => string.Join(" ",x))
                .ToList();

            return explodedAddresses;
        }

        /// <summary>
        /// Sorts addresses by frequency descending and then alphabetically ascending from the currently loaded CSV file
        /// </summary>
        /// <returns>
        /// A dictionary of names and their occurences sorted by descending frequency  and then alphabetically ascending
        /// </returns>
        public Dictionary<string, int> SortNames()
        {
            
            var aggregate = new Dictionary<string, int>();
            if (Rows == default(Dictionary<string, int>))
                return aggregate;
            var names = Rows.Select(x =>  x.FirstName ).ToList();
            var lastNames = Rows.Select(x => x.LastName ).ToList();
            names.AddRange(lastNames);
            aggregate = names.GroupBy(x => x)
                  .OrderByDescending(g => g.Count())
                  .ThenBy(g => g.Key)
                  .Select(g => new {  Key = g.Key ,  Value = g.Count()})
                  .ToDictionary(d => d.Key, d => d.Value);

            return aggregate;
        }

        /// <summary>
        /// Writes the sorted names to an ouput file
        /// </summary>
        /// <param name="filename">
        /// Path to the output file
        /// </param>
        public void WriteNamesToFile(string filename)
        {
            var content = "";
            foreach(KeyValuePair<string,int> row in SortNames())
            {
                content += row.Key + " " + row.Value.ToString()+"\n";
            }
            _csvReaderAdapter.WriteTextFile(content, filename);
        }

        /// <summary>
        /// Writes the sorted addresses to an ouput file
        /// </summary>
        /// <param name="filename">
        /// Path to the output file
        /// </param>
        public void WriteAddressesToFile(string filename)
        {
            var content = "";
            foreach (string address in SortAddresses())
            {
                content += address + "\n";
            }
            _csvReaderAdapter.WriteTextFile(content, filename);
        }
    }
}
