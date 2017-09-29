using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReadCSV.Core
{
    public class CSVFileDescriptor 
    {
        public List<string> Header { get; set; } = new List<string>();
        public List<Dictionary<string, string>> Rows { get; set; } = new List<Dictionary<string, string>>();
    }
}
