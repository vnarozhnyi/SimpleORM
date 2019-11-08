using System.Collections.Generic;
using System.Data.SqlClient;

namespace ORM
{
    internal class Repository
    {
        public SqlConnection _dbconnection;
        public List<Users> _users;

        public Repository(SqlConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public void Update(ORMModel ormModel)
        {
            ormModel.Update(_dbconnection);
        }

        public void Delete(ORMModel ormModel)
        {
            ormModel.Delete(_dbconnection);
        }

        public void Insert(ORMModel ormModel)
        {
            ormModel.Insert(_dbconnection);
        }

        public List<Users> Users
        {
            get
            {
                return ((new Users()).Select<Users>(_dbconnection));
            }
        }

    }
}
