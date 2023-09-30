using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using person_money.Models;

namespace person_money
{
    public class UsersController : Controller
    {
        private readonly PersonMoneyContext _context;

        public UsersController(PersonMoneyContext context)
        {
            _context = context;
        }

         public async Task<IActionResult> Index()
        {
          
            var personMoneyContext = _context.Users.Include(u => u.IdRoleNavigation);
            return View(await personMoneyContext.ToListAsync());
        }

         public async Task<IActionResult> Details(int? id)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.IdRoleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

         public IActionResult Create()
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            ViewData["IdRole"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Login,Password,IdRole")] User user)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRole"] = new SelectList(_context.Roles, "Id", "Name", user.IdRole);
            return View(user);
        }

          public async Task<IActionResult> Edit(int? id)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["IdRole"] = new SelectList(_context.Roles, "Id", "Name", user.IdRole);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password,IdRole")] User user)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRole"] = new SelectList(_context.Roles, "Id", "Name", user.IdRole);
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.IdRoleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (_context.Users == null)
            {
                return Problem("Entity set 'PersonMoneyContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
       private int[] UserRoleCheck(){
        int[] arr_ids = new int[2];
        Claim claim = null;
        string val;
        try{
            claim = HttpContext.User.Claims.First(c => c.Type == ClaimsIdentity.DefaultNameClaimType);
        }catch(Exception ex){
        };
        if(claim!=null){
            string vl = claim.Value;
            string[] arr_str = vl.Split(new char[] { ',' });
            arr_ids[0] = Int32.Parse(arr_str[0]);
            arr_ids[1] = Int32.Parse(arr_str[1]);
            return arr_ids;
        }
        return null;
    }  
    }
}
