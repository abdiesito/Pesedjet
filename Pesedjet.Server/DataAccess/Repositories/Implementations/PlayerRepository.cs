using Microsoft.EntityFrameworkCore;
using Pesedjet.Server.DataAccess.Data;
using Pesedjet.Server.Models;
using Pesedjet.Server.DataAccess.Repositories.Interfaces;

namespace Pesedjet.Server.DataAccess.Repositories.Implementations;

public class PlayerRepository(PesedjetDatabaseContext context) : IPlayerRepository
{
    public async Task<Player?> GetPlayerByUsernameAsync(string username)
    {
        return await context.Players
            .Include(p => p.Profile)
            .FirstOrDefaultAsync(p => p.Username == username);
    }
}