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
    public class MQTT_TopicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MQTT_TopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MQTT_Topics
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Topics.Include(m => m.Broker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MQTT_Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mQTT_Topic = await _context.Topics
                .Include(m => m.Broker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mQTT_Topic == null)
            {
                return NotFound();
            }

            return View(mQTT_Topic);
        }

        // GET: MQTT_Topics/Create
        public IActionResult Create()
        {
            ViewData["BrokerId"] = new SelectList(_context.Brokers, "Id", "Id");
            return View();
        }

        // POST: MQTT_Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrokerId,Topic,QoS")] MQTT_Topic mQTT_Topic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mQTT_Topic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrokerId"] = new SelectList(_context.Brokers, "Id", "Id", mQTT_Topic.BrokerId);
            return View(mQTT_Topic);
        }

        // GET: MQTT_Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mQTT_Topic = await _context.Topics.FindAsync(id);
            if (mQTT_Topic == null)
            {
                return NotFound();
            }
            ViewData["BrokerId"] = new SelectList(_context.Brokers, "Id", "Id", mQTT_Topic.BrokerId);
            return View(mQTT_Topic);
        }

        // POST: MQTT_Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrokerId,Topic,QoS")] MQTT_Topic mQTT_Topic)
        {
            if (id != mQTT_Topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mQTT_Topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MQTT_TopicExists(mQTT_Topic.Id))
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
            ViewData["BrokerId"] = new SelectList(_context.Brokers, "Id", "Id", mQTT_Topic.BrokerId);
            return View(mQTT_Topic);
        }

        // GET: MQTT_Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mQTT_Topic = await _context.Topics
                .Include(m => m.Broker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mQTT_Topic == null)
            {
                return NotFound();
            }

            return View(mQTT_Topic);
        }

        // POST: MQTT_Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mQTT_Topic = await _context.Topics.FindAsync(id);
            _context.Topics.Remove(mQTT_Topic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MQTT_TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
    }
}
