using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ORMTestDB;Integrated Security=True";

            UnitOfWork uow = new UnitOfWork(connectionString);
        
            uow.repository.Insert(new User { Id = 1, Name = "Admin" });
            uow.repository.Insert(new User { Id = 2, Name = "User" });
            uow.repository.Insert(new User { Id = 3, Name = "Name" });
            uow.repository.Update(new User { Id = 3, Name = "Admin" });
            uow.repository.Delete(new User { Id = 3 });
            uow.repository.Insert(new User { Id = 3, Name = "Hello" });
        }
    }
}
