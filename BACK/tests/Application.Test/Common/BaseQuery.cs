using System;
using Infrastructure.Persistence;
using Xunit;

namespace Application.Test.Common
{
    public sealed class BaseQuery : IDisposable
    {
        public DataContext Context { get; }

        public BaseQuery()
        {
            Context = DataContextFactory.Create();
        }

        public void Dispose()
        {
            DataContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryTests")]
    public class QueryCollection : ICollectionFixture<BaseQuery> { }
}
