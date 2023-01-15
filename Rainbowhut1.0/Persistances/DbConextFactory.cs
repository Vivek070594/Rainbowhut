using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Rainbowhut1._0.Persistances
{
    public class DbContextFactory : IAppDbContextFactory<ApplicationDbContext>
    {
        private readonly IConfiguration _configuration;

        public const string DefaultDbConnectionStringName = "db:bmsprime";

        /// <summary>
        /// Constructor used in DI at runtime.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Factory used in DI at runtime.
        /// </summary>
        /// <returns>A new AppDbContext instance.</returns>
        public ApplicationDbContext Create()
        {
            var dbConnectionString = _configuration.GetSection(DefaultDbConnectionStringName).Value;
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(dbConnectionString)
                .Options;
            return new ApplicationDbContext(dbOptions);
        }

        //private string GetDbConnectionString(string connectionStringWithoutDbName)
        //{
        //    var dbName = string.Join("_", ProductName.ToUpperInvariant(), customer.ToUpperInvariant(), ServiceName.ToUpperInvariant());
        //    return connectionStringWithoutDbName + dbName;
        //}
    }
}
