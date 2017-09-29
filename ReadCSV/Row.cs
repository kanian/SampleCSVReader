using System;
using System.Collections.Generic;
using System.Text;

namespace ReadCSV.Core
{
    /// <summary>
    /// Represents persons record
    /// </summary>
    public class Row : Record
    {
        /// <value>
        /// Gets or sets the firstname
        /// </value>
        public string FirstName { get; set; }
        /// <value>
        /// Gets or sets the lastname
        /// </value>
        public string LastName { get; set; }
        /// <value>
        /// Gets or sets the address
        /// </value>
        public string Address { get; set; }
        /// <value>
        /// Gets or sets the phone number
        /// </value>
        public string PhoneNumber { get; set; }
    }
}
