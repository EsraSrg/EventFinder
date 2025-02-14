

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class AppUser: IdentityUser
{

    public string? Name  { get; set; }
    public int? Age { get; set; }
    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public Address? Adress { get; set; }

    

}