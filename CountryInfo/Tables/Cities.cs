using System;
using System.Data.Linq.Mapping;

namespace CountryInfo
{
    [Table]
    internal class Cities
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public String Title { get; set; }
    }
}
