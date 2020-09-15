using System;
using System.Data.Linq.Mapping;

namespace CountryInfo
{
    [Table]
    internal class Countries
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public String Title { get; set; }
        [Column]
        public String Code { get; set; }
        [Column]
        public int Capital { get; set; }
        [Column]
        public Double Area { get; set; }
        [Column]
        public int Population { get; set; }
        [Column]
        public int Region { get; set; }
    }
}
