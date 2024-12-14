public interface IEventRepository{

    Task<IEnumerable<Event>> GetAll();
    Task<Event> GetByIdAsync(int id);
    Task<IEnumerable<Event>> GetEventByCity(string city);

    bool Add(Event foundEvent);
    bool Update(Event foundEvent);
    bool Delete(Event foundEvent);
    bool Save();


}