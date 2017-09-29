using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReadCSV.Core;
using ReadCSV.Core.AdapterInterfaces;
using ReadCSVBusinessLayer;
using ReadCSVDAL;
using ReadCSVDependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReadCSVTests.Integration
{
    [TestClass]
    public partial class ReadCSVTests
    {
        [TestMethod]
        public void NamesAreProperlySorted()
        {
            // Arrange
            var csvReaderAdapter = DependencyInjections.Instance.ServiceProvider.GetService(typeof(ICSVReader<Row>));
            var readCSVService = new ReadCSVService((ICSVReader<Row> )csvReaderAdapter,"./TestData/testcsv.csv");
            var toCompare = Data.SortedNames.Select(x => x.Key)
                .ToList();
            // Act
            var sortedNames = readCSVService.SortNames().Select(x => x.Key)
                .ToList();
            // Assert
            Assert.IsTrue(
                sortedNames.Count == toCompare.Count &&
                string.Join("", sortedNames) == string.Join("", toCompare)
                );
        }

        [TestMethod]
        public void AddressesAreProperlySorted()
        {
            // Arrange
            var csvReaderAdapter = DependencyInjections.Instance.ServiceProvider.GetService(typeof(ICSVReader<Row>));
            var readCSVService = new ReadCSVService((ICSVReader<Row>)csvReaderAdapter, "./TestData/testcsv.csv");
            var toCompare = Data.SortedAddresses;
            var chainedAddressesToCompareTo = string.Join("", toCompare);
            // Act
            var sortedAddresses = readCSVService.SortAddresses();
            var chainedSortedAddresses = string.Join("", sortedAddresses);
            // Assert
            Assert.IsTrue(
                sortedAddresses.Count == toCompare.Count &&
                chainedSortedAddresses == chainedAddressesToCompareTo
                );
        }

        [TestMethod]
        public void NamesAreCorrectlyWrittenToOutputFile()
        {
            // Arrange
            var csvReaderAdapter = DependencyInjections.Instance.ServiceProvider.GetService(typeof(ICSVReader<Row>));
            var readCSVService = new ReadCSVService((ICSVReader<Row>)csvReaderAdapter, "./TestData/testcsv.csv");
            var toCompare =  System.IO.File.ReadAllText("./TestData/testoutput.txt");
            toCompare = Regex.Replace(toCompare, @"\t|\n|\r", "");
            // Act
            readCSVService.WriteNamesToFile("./TestData/TestOutput/output.txt");
            var result = System.IO.File.ReadAllText("./TestData/TestOutput/output.txt");
            result = Regex.Replace(result, @"\t|\n|\r", "");
            // Assert
            Assert.IsTrue(
                toCompare == result
                );
        }

        [TestMethod]
        public void AddressesAreCorrectlyWrittenToOutputFile()
        {
            // Arrange
            var csvReaderAdapter = DependencyInjections.Instance.ServiceProvider.GetService(typeof(ICSVReader<Row>));
            var readCSVService = new ReadCSVService((ICSVReader<Row>)csvReaderAdapter, "./TestData/testcsv.csv");
            var toCompare = System.IO.File.ReadAllText("./TestData/testaddressoutput.txt");
            toCompare = Regex.Replace(toCompare, @"\t|\n|\r", "");
            // Act
            readCSVService.WriteAddressesToFile("./TestData/TestOutput/address_output.txt");
            var result = System.IO.File.ReadAllText("./TestData/TestOutput/address_output.txt");
            result = Regex.Replace(result, @"\t|\n|\r", "");
            // Assert
            Assert.IsTrue(
                toCompare == result
                );
        }
    }
}
