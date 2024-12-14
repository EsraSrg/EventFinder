using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext{

public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
{
    
}

public DbSet<Event> Events {get; set;}
public DbSet<Address> Addresses{get; set;}
public DbSet<AppUser> AppUsers {get; set;}

}