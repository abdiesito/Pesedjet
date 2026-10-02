namespace Pesedjet.Client.Models;

public enum FriendItemType
{
    Added,
    PendingRequest,
    Blocked
}

public class FriendItemModel
{
    public int PlayerId { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public FriendItemType ItemType { get; set; }
}