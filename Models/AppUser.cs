

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


public class AppUser : IdentityUser
{
    public string? ProfileImageUrl { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    [ForeignKey("Address")]
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
    public ICollection<Event> Events { get; set; }



}
