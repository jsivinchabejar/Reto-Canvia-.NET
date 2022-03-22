using Canvia.Infrastructure.Data;
using Canvia.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Canvia.Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly RetoCanviaContext context;

        public BaseRepository(RetoCanviaContext context)
        {
            this.context = context;
        }

        public async Task BeginTransactionAsync()
        {
            await context.Database.BeginTransactionAsync();
        }

        public async Task AddAsync(object entity)
        {
            await context.AddAsync(entity);
        }

        public void CommitTransaction()
        {
            context.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            context.Database.RollbackTransaction();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task ReloadAsync(object entity)
        {
            await context.Entry(entity).ReloadAsync();
        }
    }
}
