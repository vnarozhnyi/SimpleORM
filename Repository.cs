using System.Collections.Generic;
using System.Data.SqlClient;

namespace ORM
{
    internal class Repository
    {
        private readonly SqlConnection _dbconnection;
        private List<User> _users;
       

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


        public List<User> Users
        {
            get
            {
                return ((new User()).Select<User>(_dbconnection));
            }
        }

    }
}
