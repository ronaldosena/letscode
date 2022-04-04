using System;
using Infrastructure.Persistence;

namespace Application.Test.Common
{
    public class BaseCommand : IDisposable
    {
        public DataContext Context { get; }

        public BaseCommand()
        {
            Context = DataContextFactory.Create();
        }

        public void Dispose()
        {
            DataContextFactory.Destroy(Context);
        }
    }
}
