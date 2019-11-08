using System.Data.SqlClient;

namespace ORM
{
    class UnitOfWork
    {
        public SqlConnection _dbconnection;
        private Repository repo;

        public UnitOfWork(string connectionString)
        {
            _dbconnection = new SqlConnection(connectionString);
            _dbconnection.Open();
        }

        public Repository repository
        {
            get
            {
                if (repo == null)
                {
                    repo = new Repository(_dbconnection);
                }
                return repo;
            }
        }


    }
}
