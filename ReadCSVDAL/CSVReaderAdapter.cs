using System;
using ReadCSV.Core;
using ReadCSV.Core.AdapterInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ReadCSVDAL
{
    /// <summary>
    /// The file reader adapter
    /// </summary>
    public class CSVReaderAdapter : ICSVReader<Row>
    {
        private readonly ICSVSchema<Row> _schema;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="schema">
        /// The schema data logic
        /// </param>
        public CSVReaderAdapter(ICSVSchema<Row> schema)
        {
            _schema = schema;
        }
        /// <summary>
        /// Read CSV file from filesystem
        /// </summary>
        /// <param name="filename">
        /// Path to CSV file
        /// </param>
        /// <returns></returns>
        public string ReadCSVFile(string filename)
        {
            
            // Get file content.
            string contents = System.IO.File.ReadAllText(filename);

            return contents;
        }
        /// <summary>
        /// Parses the content of a CSV file into an header and a number of rows
        /// </summary>
        /// <param name="contents">
        /// Contents read form the CSV file
        /// </param>
        /// <returns>
        /// A CSV file descriptor
        /// </returns>
        public CSVFileDescriptor GetCSVFileDescriptor(string contents)
        {
            var descriptor = new CSVFileDescriptor();

            // Explode file into lines.
            contents = contents.Replace('\n', '\r');
            string[] lines = contents.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            // In case we don't have at least two lines, then there is no reason parsing
            if(lines.Length < 2)
            {
                return descriptor;
            }

            // Get the attributes listed in the csv file header
            string[] recordAttributes = lines[0].Split(new char[] { ',' });

            // Set csv files header attributes in the description header
            for(int i = 0; i < recordAttributes.Length; i++)
            {
                descriptor.Header.Add(recordAttributes[i]);
            }

            // Add remaining lines to the descriptor as rows
            for(int i=1; i< lines.Length;i++)
            {
                var rowDictionary = new Dictionary<string, string>();
                // Get the cell values listed in the current line
                string[] cellValues = lines[i].Split(new char[] { ',' });
                for(int j = 0; j < cellValues.Length; j++)
                {
                    rowDictionary[descriptor.Header[j]] = cellValues[j];
                }
                descriptor.Rows.Add(rowDictionary);
            }

            return descriptor;
        }
        /// <summary>
        /// Maps the contents of the CSV file to a list of records
        /// </summary>
        /// <param name="descriptor">
        /// The CSV file descriptor
        /// </param>
        /// <returns></returns>
        public IEnumerable<Record> GetRows(CSVFileDescriptor descriptor)
        {
            var hydratedRows = descriptor.Rows.Select(x => _schema.Hydrate(x)).AsEnumerable();
            return hydratedRows;    
        }

        /// <summary>
        /// Write a text file to the filesystem
        /// </summary>
        /// <param name="content">
        /// The content to write
        /// </param>
        /// <param name="filename">
        /// The path to the file
        /// </param>
        public void WriteTextFile(string content, string filename)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.File.Create(filename)))
            {
                file.WriteLine(content);
            }
        }
    }
}
