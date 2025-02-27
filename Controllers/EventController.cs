using EventFinder.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class EventController : Controller

{
    private readonly IEventRepository _eventRepository;
    private readonly IPhotoService _photoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public EventController(IEventRepository eventRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
    {

        _eventRepository = eventRepository;
        _photoService = photoService;
        _httpContextAccessor = httpContextAccessor;
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
        var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
        var CreateEventViewModel = new CreateEventViewModel { AppUserId = curUserId };
        return View(CreateEventViewModel);
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
                AppUserId = eventVM.AppUserId,
                Address = new Address
                {

                    City = eventVM.Address.City,
                    State = eventVM.Address.State,
                    Street = eventVM.Address.Street

                }
            };
            _eventRepository.Add(foundEvent);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Photo Upload Failed");
        }
        return View(eventVM);

    }

    public async Task<IActionResult> Edit(int id) // View aynı isimde olmalı Edit.cshtml
    {
        var foundEvent = await _eventRepository.GetByIdAsync(id);
        if (foundEvent == null) return View("Error");

        var eventVM = new EditEventViewModel
        { // mapping

            Title = foundEvent.Title,
            Description = foundEvent.Description,
            AddressId = foundEvent.AddressId,
            Address = foundEvent.Address,
            URL = foundEvent.Image,
            EventCategory = foundEvent.EventCategory
        };
        return View(eventVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditEventViewModel eventVM)
    {

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit club");
            return View("Edit", eventVM);
        }
        var userEvent = await _eventRepository.GetByIdAsyncNoTracking(id);

        if (userEvent != null)
        {
            try
            {
                await _photoService.DeletePhotoAsync(userEvent.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(eventVM);
            }

            var photoResult = await _photoService.AddPhotoAsync(eventVM.Image);

            var foundEvent = new Event
            {
                Id = id,
                Title = eventVM.Title,
                Description = eventVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = eventVM.AddressId,
                Address = eventVM.Address,


            };
            _eventRepository.Update(foundEvent);

            return RedirectToAction("Index");
        }
        else
        {
            return View(eventVM);

        }


    }
    public async Task<IActionResult> Delete(int id)
    {
        var eventDetails = await _eventRepository.GetByIdAsync(id);
        if (eventDetails == null) return View("Error");
        return View(eventDetails);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventDetails = await _eventRepository.GetByIdAsync(id);
        if (eventDetails == null) return View("Error");
       
       _eventRepository.Delete(eventDetails);
       return RedirectToAction("Index");
    }
}

