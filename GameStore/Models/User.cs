using System.Collections.Generic;

namespace GameStore.Models
{
  public class User
  {
    public User()
    {
      this.JoinEntities = new HashSet<GameUser>();
    }
      public int UserId { get; set; }
      public string Name { get; set; }
      public virtual ICollection<GameUser> JoinEntities { get; set;}
  }
}