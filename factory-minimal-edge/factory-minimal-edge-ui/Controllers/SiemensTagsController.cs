using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using factory_minimal_edge_models;
using factory_minimal_edge_ui.Data;
using S7.Net;

namespace factory_minimal_edge_ui.Controllers
{
    public class SiemensTagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiemensTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SiemensTags
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SiemensTags.Include(s => s.Device);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SiemensTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siemensTag = await _context.SiemensTags
                .Include(s => s.Device)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siemensTag == null)
            {
                return NotFound();
            }

            return View(siemensTag);
        }

        // GET: SiemensTags/Create
        public IActionResult Create()
        {
            var VariableTypes = from VarType i in Enum.GetValues(typeof(VarType))
                           select new { Id = (int)i, Name = i.ToString() };

            ViewData["VariableTypes"] = new SelectList(VariableTypes, "Id", "Name");

            ViewData["DeviceId"] = new SelectList(_context.SiemensDevices, "Id", "Name");
            return View();
        }

        // POST: SiemensTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DeviceId,Type,Address")] SiemensTag siemensTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siemensTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceId"] = new SelectList(_context.SiemensDevices, "Id", "Name", siemensTag.DeviceId);
            return View(siemensTag);
        }

        // GET: SiemensTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siemensTag = await _context.SiemensTags.FindAsync(id);
            if (siemensTag == null)
            {
                return NotFound();
            }
            ViewData["DeviceId"] = new SelectList(_context.SiemensDevices, "Id", "Name", siemensTag.DeviceId);
            return View(siemensTag);
        }

        // POST: SiemensTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DeviceId,Type,Address")] SiemensTag siemensTag)
        {
            if (id != siemensTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siemensTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiemensTagExists(siemensTag.Id))
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
            ViewData["DeviceId"] = new SelectList(_context.SiemensDevices, "Id", "Name", siemensTag.DeviceId);
            return View(siemensTag);
        }

        // GET: SiemensTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siemensTag = await _context.SiemensTags
                .Include(s => s.Device)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siemensTag == null)
            {
                return NotFound();
            }

            return View(siemensTag);
        }

        // POST: SiemensTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siemensTag = await _context.SiemensTags.FindAsync(id);
            _context.SiemensTags.Remove(siemensTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiemensTagExists(int id)
        {
            return _context.SiemensTags.Any(e => e.Id == id);
        }
    }
}
