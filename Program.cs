namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ORMTestDB;Integrated Security=True";

            UnitOfWork uow = new UnitOfWork(connectionString);

            uow.repository.Insert(new Users { Name = "Admin" });
            uow.repository.Insert(new Users { Name = "User" });
            uow.repository.Insert(new Users { Name = "Name" });
            uow.repository.Update(new Users { Name = "Admin" });
            uow.repository.Insert(new Users { Name = "Hello" });

            uow._dbconnection.Close();
        }
    }
}
