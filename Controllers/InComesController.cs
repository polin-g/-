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
    public class InComesController : Controller
    {
        private readonly PersonMoneyContext _context;

        public InComesController(PersonMoneyContext context)
        {
            _context = context;
        }

        // GET: InComes
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
            var personMoneyContext = _context.InComes.Where(n=>n.IdClient==id_user).Include(i => i.IdClientNavigation).Include(i => i.IdInComeCatNavigation);
            return View(await personMoneyContext.ToListAsync());
        }

        // GET: InComes/Details/5
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
            if (id == null || _context.InComes == null)
            {
                return NotFound();
            }

            var inCome = await _context.InComes
                .Include(i => i.IdClientNavigation)
                .Include(i => i.IdInComeCatNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inCome == null)
            {
                return NotFound();
            }

            return View(inCome);
        }

        // GET: InComes/Create
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

            ViewData["IdClient"] = id_user;
            ViewData["IdInComeCat"] = new SelectList(_context.InComeCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sum,DateIn,IdClient,IdInComeCat")] InCome inCome)
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
                _context.Add(inCome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClient"] = id_user;
            ViewData["IdInComeCat"] = new SelectList(_context.InComeCategories, "Id", "Name", inCome.IdInComeCat);
            return View(inCome);
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
            if (id == null || _context.InComes == null)
            {
                return NotFound();
            }

            var inCome = await _context.InComes.FindAsync(id);
            if (inCome == null)
            {
                return NotFound();
            }
            ViewData["IdClient"] = id_user;
            ViewData["IdInComeCat"] = new SelectList(_context.InComeCategories, "Id", "Name", inCome.IdInComeCat);
            return View(inCome);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sum,DateIn,IdClient,IdInComeCat")] InCome inCome)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;

            if (id != inCome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inCome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InComeExists(inCome.Id))
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
            ViewData["IdClient"] = id_user;
            ViewData["IdInComeCat"] = new SelectList(_context.InComeCategories, "Id", "Name", inCome.IdInComeCat);
            return View(inCome);
        }

        // GET: InComes/Delete/5
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
            if (id == null || _context.InComes == null)
            {
                return NotFound();
            }

            var inCome = await _context.InComes
                .Include(i => i.IdClientNavigation)
                .Include(i => i.IdInComeCatNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inCome == null)
            {
                return NotFound();
            }

            return View(inCome);
        }

        // POST: InComes/Delete/5
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

            if (_context.InComes == null)
            {
                return Problem("Entity set 'PersonMoneyContext.InComes'  is null.");
            }
            var inCome = await _context.InComes.FindAsync(id);
            if (inCome != null)
            {
                _context.InComes.Remove(inCome);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InComeExists(int id)
        {
          return (_context.InComes?.Any(e => e.Id == id)).GetValueOrDefault();
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
