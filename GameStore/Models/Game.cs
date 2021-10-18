using System;
using System.Collections.Generic;

namespace GameStore.Models
{
  public class Game
  {
    public Game()
    {
      this.JoinEntities = new HashSet<GameUser>();
    }

    public int GameId {get;set;}
    public string Name {get;set;}
    public virtual ICollection<GameUser> JoinEntities {get;set;}
  }
}