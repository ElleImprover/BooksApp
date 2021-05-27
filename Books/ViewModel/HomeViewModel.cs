using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.ViewModel
{
    public class HomeViewModel
    {
        public string Message { get; set; }

        public List<Books.Models.Books> Books { get; set; }

    }
}
