using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBFirstProjectEFCore.Models;

namespace DBFirstProjectEFCore.Controllers
{
    public class WatchHistoriesController : Controller
    {
        private readonly MovieStreamingDbContext _context;

        public WatchHistoriesController(MovieStreamingDbContext context)
        {
            _context = context;
        }

        // GET: WatchHistories
        public async Task<IActionResult> Index()
        {
            var movieStreamingDbContext = _context.WatchHistories.Include(w => w.Movie).Include(w => w.User);
            return View(await movieStreamingDbContext.ToListAsync());
        }

        // GET: WatchHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchHistory = await _context.WatchHistories
                .Include(w => w.Movie)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.HistoryId == id);
            if (watchHistory == null)
            {
                return NotFound();
            }

            return View(watchHistory);
        }

        // GET: WatchHistories/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: WatchHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryId,UserId,MovieId,WatchedOn")] WatchHistory watchHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watchHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", watchHistory.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", watchHistory.UserId);
            return View(watchHistory);
        }

        // GET: WatchHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchHistory = await _context.WatchHistories.FindAsync(id);
            if (watchHistory == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", watchHistory.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", watchHistory.UserId);
            return View(watchHistory);
        }

        // POST: WatchHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryId,UserId,MovieId,WatchedOn")] WatchHistory watchHistory)
        {
            if (id != watchHistory.HistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watchHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchHistoryExists(watchHistory.HistoryId))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", watchHistory.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", watchHistory.UserId);
            return View(watchHistory);
        }

        // GET: WatchHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchHistory = await _context.WatchHistories
                .Include(w => w.Movie)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.HistoryId == id);
            if (watchHistory == null)
            {
                return NotFound();
            }

            return View(watchHistory);
        }

        // POST: WatchHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchHistory = await _context.WatchHistories.FindAsync(id);
            if (watchHistory != null)
            {
                _context.WatchHistories.Remove(watchHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchHistoryExists(int id)
        {
            return _context.WatchHistories.Any(e => e.HistoryId == id);
        }
    }
}
