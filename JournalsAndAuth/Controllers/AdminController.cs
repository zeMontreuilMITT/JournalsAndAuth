using JournalsAndAuth.Areas.Identity.Data;
using JournalsAndAuth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JournalsAndAuth.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController: Controller
    {
        private readonly JournalsContext _context;
        private readonly UserManager<JournalsUser> _userManager;
        public AdminController(JournalsContext context, UserManager<JournalsUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult UserList()
        {
            return View(_context.Users.ToList());
        }

        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            JournalsUser user = _context.Users.Find(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([Bind("Id")] JournalsUser user)
        {
            try
            {
                JournalsUser foundUser = _context.Users.Find(user.Id);

                if(foundUser != null)
                {
                    await _userManager.DeleteAsync(foundUser);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(UserList));
                } else
                {
                    return NotFound();
                }
            } catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
