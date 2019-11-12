using System;
using System.Data.Linq.Mapping;

namespace ORM.Model
{
    [Serializable]
    [Table(Name = "[Ship]")]
    class Ship : ORMModel
    {
        [SqlColumn("Id", "INT NOT NULL", IsPrimaryKey = true)]
        public int Id { get; set; }

        [SqlColumn("Speed", "INT NOT NULL")]
        public int Speed { get; set; }

        [SqlColumn("Range", "INT NOT NULL")]
        public int Ragne { get; set; }

        [SqlColumn("Length", "INT NOT NULL")]
        public int Length { get; set; }

        [SqlColumn("TypeID", "INT NOT NULL")]
        public Types TypeID { get; set; }

        [SqlColumn("DirectionID", "INT NOT NULL")]
        public Directions DirectionID { get; set; }
    }
}
