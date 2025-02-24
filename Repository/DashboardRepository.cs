
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
        var curUser=_httpContextAccessor.HttpContext?.User.GetUserId();
        var userEvents= _context.Events.Where(r=>r.AppUser.Id==curUser);
        return userEvents.ToList();

    }
}