using System.Data.SqlClient;

namespace ORM
{
    class UnitOfWork
    {
        private readonly SqlConnection _dbconnection;
        private Repository repo;
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
