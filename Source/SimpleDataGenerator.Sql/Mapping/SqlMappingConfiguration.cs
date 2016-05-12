using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core.Extensions;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Core.Mapping.Interfaces;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Implementations;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Interfaces;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Implementations;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Interfaces;

namespace SimpleDataGenerator.Sql.Mapping
{
    public class SqlMappingConfiguration
    {
        private readonly Type _entityType;
        private readonly List<SqlPropertyConfiguration> _propertyConfigurations;
        private readonly List<SqlAssociationPropertyConfigurationBase> _associationPropertyConfigurations;
        public SqlMappingConfiguration(Type entityType)
        {
            _entityType = entityType;
            _propertyConfigurations = new List<SqlPropertyConfiguration>();
            _associationPropertyConfigurations = new List<SqlAssociationPropertyConfigurationBase>();
            TableName = _entityType.Name;
        }

        protected void AddPropertyConfiguration(SqlPropertyConfiguration propertyConfiguration)
        {
            _propertyConfigurations.Add(propertyConfiguration);
        }

        public IEnumerable<SqlPropertyConfiguration> PropertyConfigurations
        {
            get { return _propertyConfigurations; }
        }

        protected void AddAssociationPropertyConfiguration(
            SqlAssociationPropertyConfigurationBase associationPropertyConfiguration)
        {
            _propertyConfigurations.Add(associationPropertyConfiguration);
            _associationPropertyConfigurations.Add(associationPropertyConfiguration);
        }

        public IEnumerable<SqlAssociationPropertyConfigurationBase> AssociationPropertyConfigurations
        {
            get { return _associationPropertyConfigurations; }
        }

        public bool TryGetPropertyConfiguration(string propertyName, out SqlPropertyConfiguration propertyConfiguration)
        {
            var configuration = _propertyConfigurations.FirstOrDefault(x => x.SourcePropertyInfo.Name == propertyName);
            propertyConfiguration = configuration;
            return propertyConfiguration != null;
        }

        public void Validate()
        {
            if (_propertyConfigurations.All(x=> x.Key == KeyGenerator.None))
                throw new InvalidOperationException(String.Format("Missing primary key mapping for entity : {0}", _entityType));
        }

        public SqlPropertyConfiguration PrimaryKeyProperty
        {
            get { return _propertyConfigurations.First(x => x.Key != KeyGenerator.None); }
        }

        public Type EntityType
        {
            get { return _entityType; }
        }

        public string TableName { get; private set; }
        public void ToTable(string tableName)
        {
            TableName = tableName;
        }

        public int? NumberOfElementsToGenerate { get; private set; }

        public void SetNumberOfElementsToGenerate(int numberOfElementsToGenerate)
        {
            NumberOfElementsToGenerate = numberOfElementsToGenerate;
        }

    }

    public class SqlMappingConfiguration<TEntity> : SqlMappingConfiguration
        where TEntity : class
    {
        public SqlMappingConfiguration()
            : base(typeof(TEntity))
        {
            
        }

        #region KeyAssociation

        public ISqlKeyAssociationPropertyConfiguration<TEntity> HasAssociation(
            Expression<Func<TEntity, int?>> expression)
        {
            return CreateSqlKeyAssociationPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlKeyAssociationPropertyConfiguration<TEntity> HasAssociation(
            Expression<Func<TEntity, int>> expression)
        {
            return CreateSqlKeyAssociationPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlKeyAssociationPropertyConfiguration<TEntity> HasAssociation(
            Expression<Func<TEntity, Guid?>> expression)
        {
            return CreateSqlKeyAssociationPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlKeyAssociationPropertyConfiguration<TEntity> HasAssociation(
            Expression<Func<TEntity, Guid>> expression)
        {
            return CreateSqlKeyAssociationPropertyConfiguration(expression.GetPropertyInfo());
        }

        #endregion

        #region Properties

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, string>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, bool?>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, bool>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, byte[]>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, char?>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, char>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, DateTime?>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, DateTime>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, decimal?>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, decimal>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, double?>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, double>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, Enum>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, float?>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlPropertyConfiguration HasProperty(Expression<Func<TEntity, float>> expression)
        {
            return CreateSqlPropertyConfiguration(expression.GetPropertyInfo());
        }

        #endregion

        #region Identity

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, Guid?>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, Guid>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, int?>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, int>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, long?>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, long>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, object>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, short?>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        public ISqlIdentityPropertyConfiguration HasProperty(Expression<Func<TEntity, short>> expression)
        {
            return CreateSqlIdentityPropertyConfiguration(expression.GetPropertyInfo());
        }

        #endregion

        #region Association

        public ISqlAssociationPropertyConfiguration<TEntity, TInverse> HasAssociation<TInverse>(
                Expression<Func<TEntity, TInverse>> expression)
        {
            return CreateSqlAssociationPropertyConfiguration<TInverse>(expression.GetPropertyInfo());
        }

        #endregion

        #region HelpMethods

        private SqlAssociationPropertyConfiguration<TEntity, TInverse> CreateSqlAssociationPropertyConfiguration<TInverse>(PropertyInfo propertyInfo)
        {
            var propertyConfiguration = new SqlAssociationPropertyConfiguration<TEntity, TInverse>(propertyInfo);
            AddAssociationPropertyConfiguration(propertyConfiguration);
            return propertyConfiguration;
        }

        private SqlPropertyConfiguration CreateSqlPropertyConfiguration(PropertyInfo propertyInfo)
        {
            var propertyConfiguration = new SqlPropertyConfiguration(propertyInfo);
            AddPropertyConfiguration(propertyConfiguration);
            return propertyConfiguration;
        }

        private SqlIdentityPropertyConfiguration CreateSqlIdentityPropertyConfiguration(PropertyInfo propertyInfo)
        {
            var propertyConfiguration = new SqlIdentityPropertyConfiguration(propertyInfo);
            AddPropertyConfiguration(propertyConfiguration);
            return propertyConfiguration;
        }

        private SqlKeyAssociationPropertyConfiguration<TEntity> CreateSqlKeyAssociationPropertyConfiguration(
            PropertyInfo propertyInfo)
        {
            var propertyConfiguration = new SqlKeyAssociationPropertyConfiguration<TEntity>(propertyInfo);
            AddAssociationPropertyConfiguration(propertyConfiguration);
            return propertyConfiguration;
        }

        #endregion

    }
}
