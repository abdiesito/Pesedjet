using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pesedjet.Server.DataAccess.Data;

public class PesedjetDatabaseContextFactory : IDesignTimeDbContextFactory<PesedjetDatabaseContext>
{
    public PesedjetDatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PesedjetDatabaseContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Pesedjet;User Id=PesedjetAppUser;Password=abWEr2diels!;TrustServerCertificate=True;");

        return new PesedjetDatabaseContext(optionsBuilder.Options);
    }
}