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
    public class InComeCategoriesController : Controller
    {
        private readonly PersonMoneyContext _context;

        public InComeCategoriesController(PersonMoneyContext context)
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
                 

              return _context.InComeCategories != null ? 
                          View(await _context.InComeCategories.ToListAsync()) :
                          Problem("Entity set 'PersonMoneyContext.InComeCategories'  is null.");
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
                 

            if (id == null || _context.InComeCategories == null)
            {
                return NotFound();
            }

            var inComeCategory = await _context.InComeCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inComeCategory == null)
            {
                return NotFound();
            }

            return View(inComeCategory);
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

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] InComeCategory inComeCategory)
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
                _context.Add(inComeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inComeCategory);
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

            if (id == null || _context.InComeCategories == null)
            {
                return NotFound();
            }

            var inComeCategory = await _context.InComeCategories.FindAsync(id);
            if (inComeCategory == null)
            {
                return NotFound();
            }
            return View(inComeCategory);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] InComeCategory inComeCategory)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;

            if (id != inComeCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inComeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InComeCategoryExists(inComeCategory.Id))
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
            return View(inComeCategory);
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

            if (id == null || _context.InComeCategories == null)
            {
                return NotFound();
            }

            var inComeCategory = await _context.InComeCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inComeCategory == null)
            {
                return NotFound();
            }

            return View(inComeCategory);
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

            if (_context.InComeCategories == null)
            {
                return Problem("Entity set 'PersonMoneyContext.InComeCategories'  is null.");
            }
            var inComeCategory = await _context.InComeCategories.FindAsync(id);
            if (inComeCategory != null)
            {
                _context.InComeCategories.Remove(inComeCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InComeCategoryExists(int id)
        {
          return (_context.InComeCategories?.Any(e => e.Id == id)).GetValueOrDefault();
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
