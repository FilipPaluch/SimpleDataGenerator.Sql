using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Database.Interfaces;

namespace SimpleDataGenerator.Sql.Database.Implementations
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly string _connectionStringName;
        public DatabaseContext(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public PetaPoco.Database GetSession()
        {
            return new PetaPoco.Database(_connectionStringName);
        }
    }
}
