public interface IDashboardRepository
{

    Task<List<Event>> GetAllUserEvents();
    Task<AppUser> GetUserById(string id);
    Task<AppUser> GetByIdNoTracking(string id);
    bool Update(AppUser user);
    bool Save();
}