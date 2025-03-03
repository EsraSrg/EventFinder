using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _context;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Login()
    {
        var response = new LoginViewModel();
        return View(response);
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

        if (user != null)
        {// user is found, check found
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (passwordCheck)
            {
                // password correct,sign in
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Event");
                }
            }
            // password is incorrect
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginViewModel);
        }
        // User not found
        TempData["Error"] = "Wrong credentials. Please try again";
        return View(loginViewModel);


    }

    public IActionResult Register()
    {
        var response = new RegisterViewModel();
        return View(response);
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);
        var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

        if (user != null)
        {
            TempData["Error"] = "This email address is already in use";
            return View(registerViewModel);
        }
        var newUser = new AppUser()
        {
            Email = registerViewModel.EmailAddress,
            UserName = registerViewModel.EmailAddress
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        if (newUserResponse.Succeeded)
        {

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return RedirectToAction("Index", "Event");
        }
        // Şifre doğrulama hatalarını ModelState'e ekle
        foreach (var error in newUserResponse.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(registerViewModel);


    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Event");

    }


}