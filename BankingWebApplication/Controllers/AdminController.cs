using AutoMapper;
using Banking.Models.Models.ViewModels;
using BankingWebApplication.Data;
using BankingWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace BankingWebApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserList()
        {
            List<ApplicationUser> usersList = _db.ApplicationUsers.ToList();
            return View(usersList);
        }

        public async Task<IActionResult> CreateAccount()
        {
            //singleton
            if (!_roleManager.RoleExistsAsync(StaticDetails.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin));
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.User));
            }
            RegisterViewModel registerViewModel = new()
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };

            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> CreateAccount(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            { //mapping Register View Model and Application User
                var user = _mapper.Map<ApplicationUser>(model);

                user.CreatedAt = DateTime.Now;
                user.AccountNumber = GenerateUniqueAccountNumber(); //two generate unique acc number
                

                var result = await _userManager.CreateAsync(user, model.Password);
                //This line uses the UserManager to create the user in the database with the specified password.
                //The CreateAsync method returns a result indicating whether the creation was successful.
                if (result.Succeeded)
                {
                    if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == StaticDetails.Admin)
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.User);
                    }

                    //where to redirect ? 
                    //a notification that account created successfully then return index
                    
                    return RedirectToAction("Index", "Admin"); // Redirect to the admin page

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            model.RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i,
                Value = i
            });
            return View(model); //if account creation not successful
        }
        public string GenerateUniqueAccountNumber()
        {
            string accountNumber;
            Random random = new Random();
            do
            {
                accountNumber = random.Next(100000, 999999).ToString() + random.Next(100000, 999999).ToString();
            } while (_db.ApplicationUsers.Any(u => u.AccountNumber == accountNumber));

            return accountNumber;
        }

        public IActionResult Edit(string? userName)
        {
            if (userName == null || userName == "")
            {
                return NotFound();
            }
           
            ApplicationUser? userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == userName);
            if (userFromDb == null)
            {
                return NotFound();
            }
            EditUserViewModel userModel = _mapper.Map<EditUserViewModel>(userFromDb);
            return View(userModel);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == model.UserName);
                if (userFromDb == null)
                {
                    return NotFound();
                }

                // Update only the allowed fields
                userFromDb.Email = model.Email;
                userFromDb.PhoneNumber = model.PhoneNumber;
                userFromDb.UserName = model.UserName;

                _db.ApplicationUsers.Update(userFromDb);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(string? userName)
        {
            if (userName == null || userName == "")
            {
                return NotFound();
            }
            ApplicationUser? userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == userName);
            if (userFromDb == null)
            {
                return NotFound();
            }
            EditUserViewModel userModel = _mapper.Map<EditUserViewModel>(userFromDb);
            return View(userModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string? userName)
        {
            ApplicationUser? userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == userName);
            if (userFromDb == null)
            {
                return NotFound();
            }

            _db.ApplicationUsers.Remove(userFromDb);
            _db.SaveChanges();
            return RedirectToAction("UserList");
        }

    }
}
