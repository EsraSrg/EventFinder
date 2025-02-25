
using Microsoft.EntityFrameworkCore;

public class DashboardRepository : IDashboardRepository
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public DashboardRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;


    }
    public async Task<List<Event>> GetAllUserEvents()
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userEvents = _context.Events.Where(r => r.AppUser.Id == curUser);
        return userEvents.ToList(); // return multiple users

    }
    public async Task<AppUser> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetByIdNoTracking(string id) // if youre manipulating objects in same scope
    {
        return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync(); // firstordefault if youre returning one user
    }
    public bool Update(AppUser user)
    {
        _context.Users.Update(user);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;

    }

}