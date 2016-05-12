using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Database.Interfaces
{
    public interface IDatabaseContext
    {
        PetaPoco.Database GetSession();
    }
}
