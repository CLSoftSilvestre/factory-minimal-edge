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
    public class MQTT_BrokersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MQTT_BrokersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MQTT_Brokers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brokers.ToListAsync());
        }

        // GET: MQTT_Brokers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mQTT_Broker = await _context.Brokers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mQTT_Broker == null)
            {
                return NotFound();
            }

            return View(mQTT_Broker);
        }

        // GET: MQTT_Brokers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MQTT_Brokers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Protocol,Host,Port,ValidateCertificate,TLSEncryption,Username,Password,ClientId")] MQTT_Broker mQTT_Broker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mQTT_Broker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mQTT_Broker);
        }

        // GET: MQTT_Brokers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mQTT_Broker = await _context.Brokers.FindAsync(id);
            if (mQTT_Broker == null)
            {
                return NotFound();
            }
            return View(mQTT_Broker);
        }

        // POST: MQTT_Brokers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Protocol,Host,Port,ValidateCertificate,TLSEncryption,Username,Password,ClientId")] MQTT_Broker mQTT_Broker)
        {
            if (id != mQTT_Broker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mQTT_Broker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MQTT_BrokerExists(mQTT_Broker.Id))
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
            return View(mQTT_Broker);
        }

        // GET: MQTT_Brokers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mQTT_Broker = await _context.Brokers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mQTT_Broker == null)
            {
                return NotFound();
            }

            return View(mQTT_Broker);
        }

        // POST: MQTT_Brokers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mQTT_Broker = await _context.Brokers.FindAsync(id);
            _context.Brokers.Remove(mQTT_Broker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MQTT_BrokerExists(int id)
        {
            return _context.Brokers.Any(e => e.Id == id);
        }
    }
}
