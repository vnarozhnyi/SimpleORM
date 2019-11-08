using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Serializable]
    [System.Data.Linq.Mapping.Table(Name = "[User]")]
    public class Users : ORMModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SqlColumn("Id", "integer", IsPrimaryKey = true)]
        public int Id { get; set; }

        [SqlColumn("Name", "varchar(50) NULL")]
        public string Name { get; set; }
    }
}
