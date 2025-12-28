using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Models;
using StockMaster.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    public class UserController : BaseController
    {
        private readonly StockDbContext _context;
        private readonly IAuthService _authService;

        public UserController(StockDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .OrderBy(u => u.Username)
                .ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user, string PlainPassword)
        {
            
            if (string.IsNullOrEmpty(PlainPassword))
            {
                ModelState.AddModelError("PlainPassword", "Password is required");
                return View(user);
            }

          
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.CreateUserAsync(user, PlainPassword);
                    if (result)
                    {
                        TempData["Success"] = "User created successfully";
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Failed to create user. Username or email may already exist.");
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to create user");
                }
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user, string PlainPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Users.FindAsync(user.UserId);
                    if (existingUser != null)
                    {
                        existingUser.Username = user.Username;
                        existingUser.FullName = user.FullName;
                        existingUser.Email = user.Email;
                        existingUser.Role = user.Role;
                        existingUser.IsActive = user.IsActive;

                        
                        if (!string.IsNullOrEmpty(PlainPassword))
                        {
                            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(PlainPassword);
                        }

                        await _context.SaveChangesAsync();
                        TempData["Success"] = "User updated successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to update user");
                }
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    var currentUserId = HttpContext.Session.GetInt32("UserId");
                    if (user.UserId == currentUserId)
                    {
                        TempData["Error"] = "You cannot delete your own account";
                        return RedirectToAction("Index");
                    }

                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "User deleted successfully";
                }
            }
            catch
            {
                TempData["Error"] = "Cannot delete user. It may be in use.";
            }
            return RedirectToAction("Index");
        }
    }
}