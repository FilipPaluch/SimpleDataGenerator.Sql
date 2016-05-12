using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Mapping.SingleProperty.Interfaces
{
    public interface ISqlPropertyConfiguration
    {
        ISqlPropertyConfiguration ToColumn(string columnName);
        ISqlPropertyConfiguration Skip();
    }
}
