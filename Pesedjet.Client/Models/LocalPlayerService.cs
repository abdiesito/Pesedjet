using Microsoft.EntityFrameworkCore;
using Pesedjet.Client.Utilities;
using Pesedjet.Server.DataAccess.Data;
using Pesedjet.Server.Models;

namespace Pesedjet.Client.Models; 

public class LocalPlayerService
{
    private DbContextOptions<PesedjetDatabaseContext> GetOptions()
    {
        return new DbContextOptionsBuilder<PesedjetDatabaseContext>()
            .UseSqlServer(DatabaseConfig.ConnectionString)
            .Options;
    }

    public async Task<Player?> AuthenticatePlayerAsync(string username, string password)
    {
        using var context = new PesedjetDatabaseContext(GetOptions());
        
        return await context.Players
            .Include(p => p.Profile)
            .FirstOrDefaultAsync(p => p.Username == username);
    }

    public async Task<bool> RegisterPlayerAsync(Player newPlayer)
    {
        using var context = new PesedjetDatabaseContext(GetOptions());
        
        var userExists = await context.Players.AnyAsync(p => p.Username == newPlayer.Username || p.Email == newPlayer.Email);
        
        if (userExists)
        {
            return false;
        }

        await context.Players.AddAsync(newPlayer);
        await context.SaveChangesAsync();
        
        return true;
    }
}