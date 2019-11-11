using ORM.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ORM
{
    internal class Repository
    {
        public SqlConnection _dbconnection;
       // public List<Field> _field;

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

        public List<Field> fields 
        {
            get
            {
                return ((new Field()).Select<Field>(_dbconnection));
            }
        }

        public List<Ship> ships
        {
            get
            {
                return ((new Ship()).Select<Ship>(_dbconnection));
            }
        }

        public List<Types> types
        {
            get
            {
                return ((new Types()).Select<Types>(_dbconnection));
            }
        }

        public List<Directions> directions
        {
            get
            {
                return ((new Directions()).Select<Directions>(_dbconnection));
            }
        }

        public List<ShipsBase> shipsBases
        {
            get
            {
                return ((new ShipsBase()).Select<ShipsBase>(_dbconnection));
            }
        }

    }
}
