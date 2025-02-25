using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using EventFinder.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{

    private readonly IDashboardRepository _dashboardRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPhotoService _photoService;

    public DashboardController(IDashboardRepository dashboardRepository,
    IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
    {
        _dashboardRepository = dashboardRepository;
        _httpContextAccessor = httpContextAccessor;
        _photoService = photoService;
    }
    private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
    {
        user.Id = editVM.Id;
        user.City = editVM.City;
        user.State = editVM.State;
        user.ProfileImageUrl = photoResult.Url.ToString();

    }
    public async Task<IActionResult> Index()
    {
        var userEvents = await _dashboardRepository.GetAllUserEvents();
        var dashboardViewModel = new DashboardViewModel()
        {

            Events = userEvents
        };

        return View(dashboardViewModel);

    }

    public async Task<IActionResult> EditUserProfile()
    {
        var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        var user = await _dashboardRepository.GetUserById(curUserId);
        if (user == null) return View("Error");
        var editUserViewModel = new EditUserDashboardViewModel()
        {
            Id = curUserId,
            ProfileImageUrl = user.ProfileImageUrl,
            City = user.City,
            State = user.State
        };
        return View(editUserViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit profile");
            return View("EditUserProfile", editVM);
        }
        var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

        if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null) // ef is , pulling record from db and turning into an object so that we can manipulate it
        {
            var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
            // Optimistic Concurrency-"Tracking Error"
            // Use No tracking
            // var userTracking = new AppUser()
            // {

            // }
            // do this instead
            MapUserEdit(user, editVM, photoResult);
            _dashboardRepository.Update(user);
            return RedirectToAction("Index");
        }
        else
        {
            try
            {
                await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Could not delete photo");
                return View(editVM);
            }
            var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
            MapUserEdit(user, editVM, photoResult);
            _dashboardRepository.Update(user);
            return RedirectToAction("Index");
        }
    }
}