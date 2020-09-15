using System;
using System.Data.Linq.Mapping;

namespace CountryInfo
{
    [Table]
    internal class Regions
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public String Title { get; set; }
    }
}
