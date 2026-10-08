using System;

namespace Pesedjet.Server.Models;

public class TwoFactorAuthenticator
{
    public int Id { get; set; }
    public int? PlayerId { get; set; }
    public string Status { get; set; } = null!;
    public string HashCode { get; set; } = null!;
    public string Purpose { get; set; } = null!;
    public DateTime ExpireDate { get; set; }
    public bool WasUsed { get; set; } = false;

    public Player? Player { get; set; }
}