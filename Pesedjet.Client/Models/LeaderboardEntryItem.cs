namespace Pesedjet.Client.Models;

public class LeaderboardEntryItem
{
    public int Position { get; set; }

    public string Gametag { get; set; } = string.Empty;

    public string? ProfilePictureUrl { get; set; }

    public int VictoriesCount { get; set; }

    public int AmuletsCount { get; set; }

    public bool IsCurrentPlayer { get; set; }

    public bool IsFriend { get; set; }
}