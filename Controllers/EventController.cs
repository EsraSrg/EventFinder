using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class EventController:Controller

{
    private readonly IEventRepository _eventRepository;
    public EventController(IEventRepository eventRepository)
    {
    
        _eventRepository=eventRepository;
    }
public async Task<IActionResult> Index()
{
    IEnumerable<Event> events=await _eventRepository.GetAll();
    return View(events);
}
public async Task<IActionResult> Detail(int id){

    Event foundEvent =await _eventRepository.GetByIdAsync(id);
    
    return View(foundEvent);

}
}