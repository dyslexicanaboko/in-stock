namespace InStock.Lib.Entities
{
  public class RefreshTokenEntity
  {
    public Guid RefreshTokenId { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; }
    
    public DateTime ExpiresOn { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public string CreatedByIp { get; set; }
    
    //Not convinced I need any of this
    //public DateTime? Revoked { get; set; }
    //public string RevokedByIp { get; set; }
    //public string ReplacedByToken { get; set; }
    //public string ReasonRevoked { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    //public bool IsRevoked => Revoked != null;
    //public bool IsActive => !IsRevoked && !IsExpired;
  }
}
