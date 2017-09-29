using ReadCSV.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadCSV.ServiceInterfaces
{
    /// <summary>
    /// Interface to a service to read and process CSV files
    /// </summary>
    /// <typeparam name="TModel">
    /// Type of the model to represent the records
    /// </typeparam>
    public interface IReadCSVService<TModel> where TModel:Record, new()
    {
        Dictionary<string, int> SortNames();
        List<string> SortAddresses();
        void WriteNamesToFile(string filename);
        void WriteAddressesToFile(string filename);
    } 
}
