using Microsoft.AspNetCore.Identity;

namespace GameStore.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string EmailAddress { get; set; }
    public string Username { get; set; }
  }
}