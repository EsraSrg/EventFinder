using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



public class AppDbContext:IdentityDbContext<AppUser>
{

public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
{
    
}

public DbSet<Event> Events {get; set;}
public DbSet<Address> Addresses{get; set;}


}
