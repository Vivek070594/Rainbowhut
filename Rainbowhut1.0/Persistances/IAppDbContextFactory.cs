
namespace Rainbowhut1._0.Persistances
{
    public interface IAppDbContextFactory<out TContext>
        where TContext: ApplicationDbContext
    {
        TContext Create();
    }
}
