using Microsoft.EntityFrameworkCore;
using Pesedjet.Server.DataAccess.Data;
using Pesedjet.Server.Models;

namespace Pesedjet.Client.Models; 

public class LocalPlayerService
{
    public async Task<Player?> AuthenticatePlayerAsync(string username, string password)
    {
        var options = new DbContextOptionsBuilder<PesedjetDatabaseContext>()
            .UseSqlServer("Server=localhost,1433;Database=Pesedjet;User Id=PesedjetAppUser;Password=abWEr2diels!;TrustServerCertificate=True;")
            .Options;

        using var context = new PesedjetDatabaseContext(options);
        
        return await context.Players
            .Include(p => p.Profile)
            .FirstOrDefaultAsync(p => p.Username == username);
    }
}