namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ORMTestDB;Integrated Security=True";

            UnitOfWork uow = new UnitOfWork(connectionString);

            uow.repository.Insert(new Users { Id = 1, Name = "Admin" });
            uow.repository.Insert(new Users { Id = 2, Name = "User" });
            uow.repository.Insert(new Users { Id = 3, Name = "Name" });
            uow.repository.Update(new Users { Id = 3, Name = "Admin" });
            uow.repository.Delete(new Users { Id = 3 });
            uow.repository.Insert(new Users { Id = 3, Name = "Hello" });

            uow._dbconnection.Close();
        }
    }
}
