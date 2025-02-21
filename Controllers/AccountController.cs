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
        
        var user=await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

        if(user!=null)
        {// user is found, check found
            var passwordCheck=await _userManager.CheckPasswordAsync(user,loginViewModel.Password);
            if(passwordCheck)
            {
                // password correct,sign in
                var result=await _signInManager.PasswordSignInAsync(user,loginViewModel.Password,false,false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index","Event");
                }
            }
            // password is incorrect
            TempData["Error"]="Wrong credentials. Please try again";
            return View(loginViewModel);
        }
        // User not found
        TempData["Error"]="Wrong credentials. Please try again";
        return View(loginViewModel);


    }
}