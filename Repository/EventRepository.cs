
using Microsoft.EntityFrameworkCore;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;
    public EventRepository(AppDbContext context) // db gelsin
    {
        _context=context;
    }
    public bool Add(Event foundEvent)
    {
        _context.Add(foundEvent);
        return Save();
    }

    public bool Delete(Event foundEvent)
    {
        _context.Remove(foundEvent);
        return Save();
    }

    public async Task<IEnumerable<Event>> GetAll()
    {
        return await _context.Events.ToListAsync(); // get Events
    }

    public async Task<Event> GetByIdAsync(int id)
    {
        return await _context.Events.Include(a=>a.Address).FirstOrDefaultAsync(s=>s.Id==id); // get  event by id
    }
      public async Task<Event> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Events.Include(a=>a.Address).AsNoTracking().FirstOrDefaultAsync(s=>s.Id==id); // get  event by id
    }

    public async Task<IEnumerable<Event>> GetEventByCity(string city)
    {
        return await _context.Events.Where(c=>c.Address.City.Contains(city)).ToListAsync();
    }

    public bool Save()
    {
       var saved= _context.SaveChanges();
       return saved>0 ?true:false;

    }

    public bool Update(Event foundEvent)
    {
        _context.Update(foundEvent);
        return Save();
    }
}