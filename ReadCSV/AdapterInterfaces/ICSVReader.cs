using System;
using System.Collections.Generic;
using System.Text;

namespace ReadCSV.Core.AdapterInterfaces
{
    /// <summary>
    /// Interface to the adapter to read CSV files and write files
    /// </summary>
    /// <typeparam name="TModel">
    /// Type of the model to represent the records
    /// </typeparam>
    public interface ICSVReader<TModel> where TModel: Record, new()
    {
        string ReadCSVFile(string filename);
        void WriteTextFile(string content, string filename);
        CSVFileDescriptor GetCSVFileDescriptor(string filename);
        IEnumerable<Record> GetRows(CSVFileDescriptor descriptor);
    }
}
