using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ReadCSV.Core
{
    /// <summary>
    /// Interface to the schema data logic used by an adapter
    /// </summary>
    /// <typeparam name="TModel">
    /// Type of the model to represent the records
    /// </typeparam>
    public interface ICSVSchema<TModel> where TModel:new()
    {
        TModel Hydrate(Dictionary<string, string> csvRecord);
    }
}
