using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Books.ViewModel;


namespace Books.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Books.Models.Books> bookList = new List<Books.Models.Books>();

            using (var conn = new SqlConnection("Server=.;Database=books; Trusted_Connection=True;"))
            {
                conn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM books";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var book = new Books.Models.Books();
                    book.Title = reader["Title"].ToString();
                    book.ID = Convert.ToInt32(reader["BookId"]);
                    bookList.Add(book);
                }

                var vm = new HomeViewModel();
                vm.Message = "Look at these wonderful books!";
                vm.Books = bookList;
                return View(vm);
            }
        }

        public IActionResult Privacy()
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
