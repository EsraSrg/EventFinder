
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public bool Add(AppUser user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(AppUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<AppUser> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public bool Save()
    {
        var saved =  _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public async Task<bool> Update(AppUser user)
    {
       _context.Update(user);
       return Save();
    }

    bool IUserRepository.Update(AppUser user)
    {
        throw new NotImplementedException();
    }
   
}