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
    public class RolesController : Controller
    {
        private readonly PersonMoneyContext _context;

        public RolesController(PersonMoneyContext context)
        {
            _context = context;
        }

        // GET: Roles
         public async Task<IActionResult> Index()
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
              return _context.Roles != null ? 
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'PersonMoneyContext.Roles'  is null.");
        }

        // GET: Roles/Details/5
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
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
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
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Role role)
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
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
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
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Role role)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.Id))
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
            return View(role);
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
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
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
            if (_context.Roles == null)
            {
                return Problem("Entity set 'PersonMoneyContext.Roles'  is null.");
            }
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
          return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
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
