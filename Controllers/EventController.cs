using EventFinder.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class EventController : Controller

{
    private readonly IEventRepository _eventRepository;
    private readonly IPhotoService _photoService;
    public EventController(IEventRepository eventRepository, IPhotoService photoService)
    {

        _eventRepository = eventRepository;
        _photoService = photoService;
    }
    public async Task<IActionResult> Index()
    {
        IEnumerable<Event> events = await _eventRepository.GetAll();
        return View(events);
    }
    public async Task<IActionResult> Detail(int id)
    {

        Event foundEvent = await _eventRepository.GetByIdAsync(id);

        return View(foundEvent);

    }

    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventViewModel eventVM)
    {

        if (ModelState.IsValid) // modelde tanımlı kurallara uygunsa
        {
            var result = await _photoService.AddPhotoAsync(eventVM.Image);
            var foundEvent = new Event // manuel mapleme
            {
                Title = eventVM.Title,
                Description = eventVM.Description,
                Image = result.Url.ToString(),
                Address= new Address{

                    City=eventVM.Address.City,
                    State=eventVM.Address.State,
                    Street=eventVM.Address.Street

                }
            };
            _eventRepository.Add(foundEvent);
             return RedirectToAction("Index");
        }
        else{
            ModelState.AddModelError("","Photo Upload Failed");
        }
        return View(eventVM);

    }
}

