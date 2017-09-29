using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace ReadCSV.Core
{
    /// <summary>
    /// Base class for all records in the app.
    /// </summary>
    /// <remarks>
    /// Implements IEquatable. The implementation is only suited for objects of 
    /// the properties are scalar type.
    /// What we want to say two objects is to say that two records contain the same values.
    /// </remarks>
    public class Record : IEquatable<Record> 
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Record()
        {
        }

        /// <summary>
        /// Implements IEquatable<>.Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns>
        /// true if the two objects contain the same value.
        /// false otherwise.
        /// </returns>
        public bool Equals(Record other)
        {
            bool equal = true;
            foreach (PropertyInfo prop in other.GetType().GetProperties())
            {
                var val = prop.GetValue(other);
                var thisVal = this.GetType().GetProperty(prop.Name).GetValue(this);
                equal &= val == thisVal;
            }
            return equal;
        }
    }
}
