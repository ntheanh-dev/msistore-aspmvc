using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Location
    {
        public long Id { get; set; }
        public string StoreName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
