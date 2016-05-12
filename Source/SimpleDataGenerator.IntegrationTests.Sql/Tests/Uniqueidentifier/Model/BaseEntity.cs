using System;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier.Model
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
