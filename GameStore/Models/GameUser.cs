namespace GameStore.Models
{
  public class GameUser
  {
    public int GameUserId { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public virtual User User { get; set; }
    public virtual Game Game { get; set; }
  }
}