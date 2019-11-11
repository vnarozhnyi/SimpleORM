using System;
using System.Data.Linq.Mapping;

namespace ORM.Model
{
    [Serializable]
    [Table(Name = "[Directions]")]
    class Directions : ORMModel
    {
        [SqlColumn("Id", "INT NOT NULL", IsPrimaryKey = true)]
        public int Id { get; set; }

        [SqlColumn("Direction", "VARCHAR(50) NOT NULL")]
        public string Direction { get; set; }
    }
}
