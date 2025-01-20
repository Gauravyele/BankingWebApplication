using Banking.Models.Models.ViewModels;
using BankingWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingWebApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //adding Identity provides helper methods which are automatically injected in the appln 
        //directly access them using constructor by dependency injection

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) //DI through constructor
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnurl = null)
        {
            if (!_roleManager.RoleExistsAsync(StaticDetails.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin));
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.User));
            }

            //To display all the roles that are there in the db and retrieve them using role manager
            ViewData["ReturnUrl"] = returnurl;

            RegisterViewModel registerViewModel = new()
            {
                RoleList = _roleManager.Roles.Select(x=> x.Name).Select(i=>new SelectListItem
                {
                    Text=i,
                    Value=i
                })
            };
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        //aync Task - as we use await
        public async Task<IActionResult> Register(RegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");

            //using helper method to redirect the user
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    DateOfBirth = model.DateOfBirth.Date,
                    AadharNumber = model.AadharNumber,
                    CreatedAt = DateTime.Now

                };

                //created a new appln user, so now add this to our table ASP.NET Users
                //we have helper methods as below

                var result=await _userManager.CreateAsync(user,model.Password); // automatically hash pwd and create that user and populate an entry in the table

                //if registration successful, sign in the user
                if (result.Succeeded)
                {

                    //to assign roles to user if succeded
                    if(model.RoleSelected!=null && model.RoleSelected.Length>0 && model.RoleSelected == StaticDetails.Admin)
                    {
                        //using helper method present in userManager
                        await _userManager.AddToRoleAsync(user,StaticDetails.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.User);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false); // isPersistent - false says remember me
                    //return RedirectToAction("Index", "Home");
                    return LocalRedirect(returnurl);
                }
                AddErrors(result);

            }

            model.RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            }); //to populate the role list again and return back to view in case of failure

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnurl = null)
        {
            ViewData["ReturnUrl"]=returnurl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            //using helper method to redirect the user
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
                //PasswordSignInAsync - will automatically call the sign in async and sign in that user
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);  // automatically hash pwd and create that user and populate an entry in the table

                //if registration successful, sign in the user
                // so we don't want to call  await _signInManager.SignInAsync(user, isPersistent: false); --> this again
                if (result.Succeeded)
                {
                    //return RedirectToAction("Index", "Home");
                    return LocalRedirect(returnurl);
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}
