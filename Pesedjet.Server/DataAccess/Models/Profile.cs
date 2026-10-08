
namespace Pesedjet.Server.Models;

public class Profile
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public string? UrlProfilePicture { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = "Offline";
    public DateTime? LastConnection { get; set; }
    public int TotalExp { get; set; } = 0;

    public Player Player { get; set; } = null!;
}