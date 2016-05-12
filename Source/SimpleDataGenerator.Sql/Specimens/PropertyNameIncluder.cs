using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;

namespace SimpleDataGenerator.Sql.Specimens
{
    public class PropertyNameIncluder : ISpecimenBuilder
    {
        private readonly List<string> _names = new List<string>();

        public PropertyNameIncluder()
        {

        }

        public PropertyNameIncluder(params string[] names)
        {
            _names = names.ToList();
        }

        public PropertyNameIncluder(IEnumerable<string> names)
        {
            _names = names.ToList();
        }

        public void Add(string name)
        {
            _names.Add(name);
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;

            if (propInfo == null) return new NoSpecimen(request);

            if (!_names.Contains(propInfo.Name)) return new OmitSpecimen();

            return new NoSpecimen(request);

        }
    }
}
