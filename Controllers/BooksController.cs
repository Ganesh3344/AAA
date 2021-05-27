using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Books.Models;
using Books.ViewModels;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController()
        {
            _context = ApplicationDbContext.Create();
        }

        // GET: Books
        public ActionResult Index()
        {
            var books = _context.Books.Include(m=>m.Category).ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            var viewModel = new BookFormViewModel
            {
                 Categories = _context.Categories.Where(m=>m.IsActive).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories.Where(m => m.IsActive).ToList();
                return View("Create",model);
            }

            var book = new Book
            {
                Title = model.Title,
                CategoryId = model.CategoryId,
                Author = model.Author,
                Description = model.Description
            };

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}