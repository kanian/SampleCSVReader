using ReadCSV.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ReadCSVTests
{
   

    public class Data
    {
        public static Dictionary<string, int> SortedNames = new Dictionary<string, int>
        {
            {"Mike",3 },
            {"Jacques",2 },
            {"Jimmy",2 },
            {"Tom",1 }
        };

        public static List<string> SortedAddresses = new List<string>
        {
            {"8 Crimson Rd" },
            {"102 Long Lane" },
            {"94 Roland St"},
            {"49 Sutherland St"},
        };

        public static List<Row> Rows = new List<Row>
        {
            new Row(){ FirstName = "John",LastName = "Doe", Address = "102 Long Lane", PhoneNumber = "29384857"},
            new Row(){ FirstName = "Jack",LastName = "Bower", Address = "007 Under Lane", PhoneNumber = "25884857"}
        };

        public static Dictionary<string, string> ReadRow = new Dictionary<string, string>()
        {
            { "FirstName","John" },
            {"LastName","Doe" },
            {"Address", "102 Long Lane" },
            {"PhoneNumber", "29384857" }
        };

        public static string CSVFileContent = "FirstName,LastName,Address,PhoneNumber\nJimmy,Smith,102 Long Lane,29384857\nClive,Owen,65 Ambling Way,31214788";

        public static CSVFileDescriptor CSVFileDescriptor = new CSVFileDescriptor
        {
            Header = new List<string>() { "FirstName", "LastName", "Address", "PhoneNumber" },
            Rows = new List<Dictionary<string, string>>()
            {
               new Dictionary<string, string>()
                {
                    { "FirstName","Jimmy" },
                    {"LastName","Smith" },
                    {"Address", "102 Long Lane" },
                    {"PhoneNumber", "29384857" }
                },
               new Dictionary<string, string>()
                {
                    { "FirstName","Clive" },
                    {"LastName","Owen" },
                    {"Address", "65 Ambling Way" },
                    {"PhoneNumber", "31214788" }
                }

            }
        };
    }
}
