using ORM.Model;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BattleShipDB;Integrated Security=True";

            UnitOfWork uow = new UnitOfWork(connectionString);

            uow.repository.Insert(new Field {Id = 1, X = 2, Y = 5 });
            uow.repository.Update(new Field {Id = 1,  X = 5, Y = 9 });
            uow.repository.Delete(new Field { Id = 1 });


            uow._dbconnection.Close();
        }
    }
}
