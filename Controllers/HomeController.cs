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
            // Get client IP address 
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();

            // Check for X-Forwarded-For header (common proxy header)
            if (HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                clientIp = forwardedFor.FirstOrDefault()?.Split(',').FirstOrDefault()?.Trim();
            }

            // Fallback to other common proxy headers
            if (string.IsNullOrEmpty(clientIp))
            {
                foreach (var header in new[] { "CF-Connecting-IP", "X-Real-IP" })
                {
                    if (HttpContext.Request.Headers.TryGetValue(header, out var headerValue))
                    {
                        clientIp = headerValue.FirstOrDefault();
                        if (!string.IsNullOrEmpty(clientIp)) break;
                    }
                }
            }

            _logger.LogInformation($"Detected client IP: {clientIp}");

            if (!string.IsNullOrEmpty(clientIp))
            {
                // Use client-specific endpoint
                string url = $"https://ipinfo.io/{clientIp}/json?token={_config.GetValue<string>("IpInfo:Token")}";
                var response = await _httpClient.GetAsync(url);

                // Log API response details
                _logger.LogInformation($"IPInfo API call: {url}");
                _logger.LogInformation($"API Status Code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var info = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Raw API response: {info}");
                    
                    ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                    
                    if (!string.IsNullOrEmpty(ipInfo.Country))
                    {
                        RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                        ipInfo.Country = myRI1.EnglishName;
                    }

                    homeViewModel.City = ipInfo.City;
                    homeViewModel.State = ipInfo.Region;

                    if (!string.IsNullOrEmpty(homeViewModel.City))
                    {
                        homeViewModel.Events = await _eventRepository.GetEventByCity(homeViewModel.City);
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"IPInfo API error: {response.StatusCode} - {errorContent}");
                }
            }
            else
            {
                _logger.LogWarning("Could not determine client IP address");
            }
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError($"Network error: {httpEx.Message}");
            if (httpEx.InnerException != null)
            {
                _logger.LogError($"Inner exception: {httpEx.InnerException.Message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"General error: {ex.Message}");
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
