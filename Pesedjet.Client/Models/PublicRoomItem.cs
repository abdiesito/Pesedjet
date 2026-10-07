namespace Pesedjet.Client.Models;

public class PublicRoomItem
{
    public string RoomCode { get; set; } = string.Empty;

    public string HostUsername { get; set; } = string.Empty;

    public int CurrentPlayers { get; set; }

    public int MaxPlayers { get; set; }

    public string GameType { get; set; } = string.Empty;

    public string CapacityDisplay => $"{CurrentPlayers}/{MaxPlayers}";
}