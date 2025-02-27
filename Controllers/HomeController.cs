using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_mvc_dotnet.Models;

namespace Project_mvc_dotnet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEventRepository _eventRepository;
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger, IEventRepository eventRepository, IConfiguration config, HttpClient httpClient)
    {
        _logger = logger;
        _eventRepository = eventRepository;
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var ipInfo = new IPInfo();
        var homeViewModel = new HomeViewModel();
        try
        {
            string url = "http://ipinfo.io?token=" + _config.GetValue<string>("IpInfo:Token");
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var info = await response.Content.ReadAsStringAsync();
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info); // taking json and turning into object
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                
                if (homeViewModel.City != null)
                {
                    homeViewModel.Events = await _eventRepository.GetEventByCity(homeViewModel.City);
                }
                else
                {
                    homeViewModel.Events = null;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"IPInfo API çağrısı başarısız: {ex.Message}");
            homeViewModel.Events = null;
        }
        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
