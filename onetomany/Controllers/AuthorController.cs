using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using onetomany.Models;

namespace onetomany.Controllers
{
    public class AuthorController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.Include(b => b.Books).ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Author author = _context.Authors.FirstOrDefault(a => a.AuthorId == id);
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author newauthor)
        {
            Author author = _context.Authors.FirstOrDefault(a=> a.AuthorId == id);
            author.Name = newauthor.Name;
            author.Country = newauthor.Country;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Authors/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            Author author = _context.Authors.FirstOrDefault(
                c => c.AuthorId == id);
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Author author = _context.Authors.FirstOrDefault(
               c => c.AuthorId == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
