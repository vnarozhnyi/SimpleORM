using System;
using System.Data.Linq.Mapping;

namespace ORM.Model
{
    [Serializable]
    [Table(Name = "[Types]")]
    class Types : ORMModel
    {
        [SqlColumn("Id", "INT NOT NULL", IsPrimaryKey = true)]
        public int Id { get; set; }

        [SqlColumn("TypeOf", "VARCHAR(50) NOT NULL")]
        public string TypeOf { get; set; }
    }
}
