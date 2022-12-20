using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using factory_minimal_edge_models;
using factory_minimal_edge_ui.Data;

namespace factory_minimal_edge_ui.Controllers
{
    public class OPC_UA_DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OPC_UA_DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OPC_UA_Devices
        public async Task<IActionResult> Index()
        {
            return View(await _context.OPC_UA_Devices.ToListAsync());
        }

        // GET: OPC_UA_Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oPC_UA_Device = await _context.OPC_UA_Devices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oPC_UA_Device == null)
            {
                return NotFound();
            }

            return View(oPC_UA_Device);
        }

        // GET: OPC_UA_Devices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OPC_UA_Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Endpoint")] OPC_UA_Device oPC_UA_Device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oPC_UA_Device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oPC_UA_Device);
        }

        // GET: OPC_UA_Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oPC_UA_Device = await _context.OPC_UA_Devices.FindAsync(id);
            if (oPC_UA_Device == null)
            {
                return NotFound();
            }
            return View(oPC_UA_Device);
        }

        // POST: OPC_UA_Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Endpoint")] OPC_UA_Device oPC_UA_Device)
        {
            if (id != oPC_UA_Device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oPC_UA_Device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OPC_UA_DeviceExists(oPC_UA_Device.Id))
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
            return View(oPC_UA_Device);
        }

        // GET: OPC_UA_Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oPC_UA_Device = await _context.OPC_UA_Devices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oPC_UA_Device == null)
            {
                return NotFound();
            }

            return View(oPC_UA_Device);
        }

        // POST: OPC_UA_Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oPC_UA_Device = await _context.OPC_UA_Devices.FindAsync(id);
            _context.OPC_UA_Devices.Remove(oPC_UA_Device);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OPC_UA_DeviceExists(int id)
        {
            return _context.OPC_UA_Devices.Any(e => e.Id == id);
        }
    }
}
