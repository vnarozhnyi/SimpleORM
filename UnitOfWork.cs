using System.Data.SqlClient;

namespace ORM
{
    class UnitOfWork
    {
        private string ConnectionString;
        private Repository repo;
        public UnitOfWork(string connectionString)
        {
           ConnectionString = connectionString;
        }

        public Repository repository
        {
            get
            {
                if (repo == null)
                {
                    repo = new Repository(ConnectionString);
                }
                return repo;
            }
        }

       
    }
}
