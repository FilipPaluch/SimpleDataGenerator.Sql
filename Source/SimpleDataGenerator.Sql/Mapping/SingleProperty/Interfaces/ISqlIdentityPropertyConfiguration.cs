using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;

namespace SimpleDataGenerator.Sql.Mapping.SingleProperty.Interfaces
{
    public interface ISqlIdentityPropertyConfiguration : ISqlPropertyConfiguration
    {
        ISqlPropertyConfiguration IsIdentity(KeyGenerator keyGenerator);
    }
}
