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
    public class SiemensDevicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiemensDevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SiemensDevices
        public async Task<IActionResult> Index()
        {
            return View(await _context.SiemensDevices.ToListAsync());
        }

        // GET: SiemensDevices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siemensDevice = await _context.SiemensDevices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siemensDevice == null)
            {
                return NotFound();
            }

            return View(siemensDevice);
        }

        // GET: SiemensDevices/Create
        public IActionResult Create()
        {
            // TODO: Add here the code for the list of PLC Types and inject into the view...

            var PlcTypes = from CpuType i in Enum.GetValues(typeof(CpuType))
                           select new { Id = (int)i, Name = i.ToString() };

            ViewData["PlcTypes"] = new SelectList(PlcTypes, "Id", "Name");

            return View();
        }

        // POST: SiemensDevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Type,IP_Address,Rack,Slot")] SiemensDevice siemensDevice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siemensDevice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siemensDevice);
        }

        // GET: SiemensDevices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siemensDevice = await _context.SiemensDevices.FindAsync(id);
            if (siemensDevice == null)
            {
                return NotFound();
            }

            var PlcTypes = from CpuType i in Enum.GetValues(typeof(CpuType))
                           select new { Id = (int)i, Name = i.ToString() };

            ViewData["PlcTypes"] = new SelectList(PlcTypes, "Id", "Name");

            return View(siemensDevice);
        }

        // POST: SiemensDevices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Type,IP_Address,Rack,Slot")] SiemensDevice siemensDevice)
        {
            if (id != siemensDevice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siemensDevice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiemensDeviceExists(siemensDevice.Id))
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
            return View(siemensDevice);
        }

        // GET: SiemensDevices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siemensDevice = await _context.SiemensDevices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siemensDevice == null)
            {
                return NotFound();
            }

            return View(siemensDevice);
        }

        // POST: SiemensDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siemensDevice = await _context.SiemensDevices.FindAsync(id);
            _context.SiemensDevices.Remove(siemensDevice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: SiemensDevices/Connect/5
        public async Task<IActionResult> Connect(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siemensDevice = await _context.SiemensDevices.FindAsync(id);
            if (siemensDevice == null)
            {
                return NotFound();
            }

            bool connectStatus = siemensDevice.Connect();

            return RedirectToAction(nameof(Index));
        }

        // GET: SiemensDevices/Connect/5
        public async Task<IActionResult> Disconnect(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siemensDevice = await _context.SiemensDevices.FindAsync(id);
            if (siemensDevice == null)
            {
                return NotFound();
            }

            bool connectStatus = siemensDevice.Disconnect();

            return RedirectToAction(nameof(Index));
        }

        private bool SiemensDeviceExists(int id)
        {
            return _context.SiemensDevices.Any(e => e.Id == id);
        }
    }
}
