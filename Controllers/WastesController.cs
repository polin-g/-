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
    public class WastesController : Controller
    {
        private readonly PersonMoneyContext _context;

        public WastesController(PersonMoneyContext context)
        {
            _context = context;
        }

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
            var personMoneyContext = _context.Wastes.Include(w => w.IdCategoryNavigation).Include(w => w.IdClientNavigation);
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
            if (id == null || _context.Wastes == null)
            {
                return NotFound();
            }

            var waste = await _context.Wastes
                .Include(w => w.IdCategoryNavigation)
                .Include(w => w.IdClientNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waste == null)
            {
                return NotFound();
            }

            return View(waste);
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
            ViewData["IdCategory"] = new SelectList(_context.WasteCategories, "Id", "Name");
            ViewData["IdClient"] = id_user;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sum,DateWaste,Description,IdCategory,IdClient")] Waste waste)
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
                _context.Add(waste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.WasteCategories, "Id", "Name", waste.IdCategory);
            ViewData["IdClient"] = id_user;
            return View(waste);
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
            if (id == null || _context.Wastes == null)
            {
                return NotFound();
            }

            var waste = await _context.Wastes.FindAsync(id);
            if (waste == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.WasteCategories, "Id", "Name", waste.IdCategory);
            ViewData["IdClient"] = id_user;
            return View(waste);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sum,DateWaste,Description,IdCategory,IdClient")] Waste waste)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (id != waste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WasteExists(waste.Id))
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
            ViewData["IdCategory"] = id_user;
            ViewData["IdClient"] = new SelectList(_context.Users, "Id", "Name", waste.IdClient);
            return View(waste);
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
            if (id == null || _context.Wastes == null)
            {
                return NotFound();
            }

            var waste = await _context.Wastes
                .Include(w => w.IdCategoryNavigation)
                .Include(w => w.IdClientNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waste == null)
            {
                return NotFound();
            }

            return View(waste);
        }

        // POST: Wastes/Delete/5
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
            if (_context.Wastes == null)
            {
                return Problem("Entity set 'PersonMoneyContext.Wastes'  is null.");
            }
            var waste = await _context.Wastes.FindAsync(id);
            if (waste != null)
            {
                _context.Wastes.Remove(waste);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WasteExists(int id)
        {
          return (_context.Wastes?.Any(e => e.Id == id)).GetValueOrDefault();
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
