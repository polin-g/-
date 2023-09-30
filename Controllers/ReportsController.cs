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
    public class ReportsController : Controller
    {
        private readonly PersonMoneyContext _context;

        public ReportsController(PersonMoneyContext context)
        {
            _context = context;
        }

        // GET: Reports
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
            var personMoneyContext = _context.Reports.Include(r => r.IdClientNavigation);
            return View(await personMoneyContext.ToListAsync());
        }

        // GET: Reports/Details/5
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
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.IdClientNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
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
            ViewData["IdClient"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClient,IncomesRep,WastesRep,Content")] Report report)
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
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClient"] = id_user;
            return View(report);
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
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["IdClient"] = id_user;
            return View(report);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdClient,IncomesRep,WastesRep,Content")] Report report)
        {
           int[] arr_ids = UserRoleCheck();
        string name = "_";
        int id_user =0;
        if(arr_ids!=null){
            name = _context.Roles.FirstOrDefault(n=>n.Id==arr_ids[0]).Name;
            id_user=arr_ids[1];

        }
        ViewBag.Role = name;
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            return View(report);
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
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.IdClientNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
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
            if (_context.Reports == null)
            {
                return Problem("Entity set 'PersonMoneyContext.Reports'  is null.");
            }
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
          return (_context.Reports?.Any(e => e.Id == id)).GetValueOrDefault();
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
