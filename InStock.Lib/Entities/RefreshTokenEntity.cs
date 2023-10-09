namespace InStock.Lib.Entities
{
  public class RefreshTokenEntity
  {
    public Guid RefreshTokenId { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; }
    
    public DateTime ExpiresOnUtc { get; set; }
    
    public DateTime CreatedOnUtc { get; set; }
    
    public string CreatedByIp { get; set; }

    public bool IsExpired() => DateTime.UtcNow >= ExpiresOnUtc;

    //Not convinced I need any of this
    //public DateTime? Revoked { get; set; }
    //public string RevokedByIp { get; set; }
    //public string ReplacedByToken { get; set; }
    //public string ReasonRevoked { get; set; }
    //public bool IsRevoked => Revoked != null;
    //public bool IsActive => !IsRevoked && !IsExpired;
  }
}
