using Book_Store.Models;
using Book_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Store.Controllers
{
    public class HomeController : Controller
    {
        
        private List<Author> authors = [new Author { Name = "Arbaz", Country = "Pak", Id = 1 }, new Author { Name = "Yahya", Country = "Pak", Id = 2 },new Author { Name = "Shayan", Country = "Pak", Id = 3 }];
        private static List<Book> books= [new Book {Title="C++ intro" ,Id=1,AuthorId=1},new Book { Title="C# in .NET ",Id=2,AuthorId=2 }];
        public IActionResult Index()
        {
            List<BookCardViewModel> bookCards = new List<BookCardViewModel>();
            foreach (var book in books)
            {
                string autName = "";
                foreach (var author in authors)
                {
                    if (author.Id == book.AuthorId)
                    {
                        autName = author.Name;
                        break;
                    }
                }

                bookCards.Add(new BookCardViewModel
                {
                    Id = book.Id,
                    AuthorName = autName,
                    Price = book.Price,
                    Title = book.Title
                });
            }

            HomeViewModel model = new HomeViewModel { Authors = authors, Books = bookCards };

            return View(model);
        }


        [HttpPost]
        public IActionResult AddBook(string Title, decimal Price, int AuthorId)
        {
            int newId = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            books.Add(new Book { Id = newId, Title = Title, Price = Price, AuthorId = AuthorId });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string Name, string Country)
        {
            int newId = authors.Any() ? authors.Max(a => a.Id) + 1 : 1;
            authors.Add(new Author { Id = newId, Name = Name, Country = Country });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            var bookToRemove = books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
