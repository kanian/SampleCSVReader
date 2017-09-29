using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReadCSV.Core;
using ReadCSVDAL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace ReadCSVTests
{
    [TestClass]
    public partial class ReadCSVTests
    {
        

        [TestMethod]
        public void CSVFileContentIsCorrectlyParsed()
        {
            // Arrange
            var fileContent = Data.CSVFileContent;
            var compareToDescriptor = Data.CSVFileDescriptor;
                // create mock schema
            var mockSchema = new Mock<ICSVSchema<Row>>();
            var csvReaderAdapter = new CSVReaderAdapter(mockSchema.Object);
            // Act
            var descriptor = csvReaderAdapter.GetCSVFileDescriptor(fileContent);
            // Assert
            Assert.IsTrue(
                descriptor.Header.Count == compareToDescriptor.Header.Count && 
                !descriptor.Header.Except(compareToDescriptor.Header).Any() &&
                descriptor.Rows.Count == compareToDescriptor.Rows.Count &&
                descriptor.Rows.Except(compareToDescriptor.Rows).Any()
                );
        }

        [TestMethod]
        public void AfterSchemaCreationSchemaSavedIsCorrect()
        {
            // Arrange
            var schema = new Dictionary<string, Type>()
            {
                { "FirstName",typeof(string)},
                { "LastName",typeof(string)},
                { "Address",typeof(string)},
                { "PhoneNumber",typeof(string)},
            };
            // Act
            var csvSchema = new CSVSchema<Row>();
            // Assert
            Assert.IsTrue((schema.Count == csvSchema.Schema.Count && !schema.Except(csvSchema.Schema).Any()));
        }

        [TestMethod]
        public void HydratedModelHasCorrectRecordValues()
        {
            // Arrange
            Dictionary<string, string> data = Data.ReadRow;
            var compareToModel = Data.Rows[0];
            var csvSchema = new CSVSchema<Row>();
            // Act
            var model = csvSchema.Hydrate(data);
            // Assert
            Assert.IsTrue(compareToModel.Equals(model));
        }
    }
}
