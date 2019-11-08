using System;
using System.Data.Linq.Mapping;

namespace ORM
{
    [Serializable]
    [Table(Name = "User")]
    public class User : ORMModel
    {
        [SqlColumn("Id", "integer", IsPrimaryKey = true)]
        public int Id { get; set; }

        [SqlColumn("Name", "varchar(100) NULL")]
        public string Name { get; set; }
    }
}
