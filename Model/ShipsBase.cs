using System;
using System.Data.Linq.Mapping;

namespace ORM.Model
{
    [Serializable]
    [Table(Name = "[ShipsBase]")]
    class ShipsBase : ORMModel
    {
        [SqlColumn("Id", "INT NOT NULL", IsPrimaryKey = true)]
        public int Id { get; set; }

        [SqlColumn("ShipID", "INT NOT NULL")]
        public int ShipID { get; set; }

        [SqlColumn("FieldID", "INT NOT NULL")]
        public int FieldID { get; set; }
    }
}
