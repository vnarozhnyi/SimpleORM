using System;
using System.Data.Linq.Mapping;

namespace ORM.Model
{
    [Serializable]
    [Table(Name = "[Field]")]
    class Field : ORMModel
    {
        [SqlColumn("Id", "INT NOT NULL", IsPrimaryKey = true)]
        public int Id { get; set; }

        [SqlColumn("X", "INT NOT NULL")]
        public int X { get; set; }

        [SqlColumn("Y", "INT NOT NULL")]
        public int Y { get; set; }
    }
}
