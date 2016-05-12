using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core;
using Ploeh.AutoFixture;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Sql.Providers;
using SimpleDataGenerator.Sql.Specimens;

namespace SimpleDataGenerator.Sql.Generators
{
    public class SimpleTypePropertyAutoFixture<TEntity>
    {
        private readonly SimpleAutoDataGenerator _dataGenerator;
        public SimpleTypePropertyAutoFixture()
        {
            _dataGenerator = new SimpleAutoDataGenerator();
        }

        public void WithConfiguration(EntityConfiguration configuration)
        {
            if (configuration != null)
            {
                _dataGenerator.WithConfiguration(configuration);
            }
        }

        public TEntity Create()
        {
            var simpleTypePropertyProvider = new ValueTypePropertyProvider();

            var simpleTypeProperties = simpleTypePropertyProvider.Get<TEntity>();

            var propertyNameIncluder = new PropertyNameIncluder();

            foreach (var simpeTypeProperty in simpleTypeProperties)
            {
                propertyNameIncluder.Add(simpeTypeProperty.Name);
            }
            _dataGenerator.Fixture.Customizations.Add(propertyNameIncluder);

            return _dataGenerator.Fixture.Create<TEntity>();
        }

    }
}
