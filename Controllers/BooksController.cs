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
            return View();
        }

        public ActionResult Create()
        {
            var viewModel = new BookFormViewModel
            {
                 Categories = _context.Categories.Where(m=>m.IsActive).ToList()
            };
            return View(viewModel);
        }

        public ActionResult Save()
        {
            throw new System.NotImplementedException();
        }
    }
}