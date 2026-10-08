
namespace Pesedjet.Server.Models;

public class Player
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string? Email { get; set; }
    public string? HashedPassword { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    
    public Profile? Profile { get; set; }
}