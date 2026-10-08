using Pesedjet.Server.Models;

namespace Pesedjet.Server.DataAccess.Repositories.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetPlayerByUsernameAsync(string username);
}