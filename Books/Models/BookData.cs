using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Books.Models
{

    public class BookData
    {
        private readonly IConfiguration _configuration;

        public BookData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Books> AllBookData()
        {
            var books = new List<Books>();

            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Books";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var title = reader["Title"].ToString();
                    var id = Convert.ToInt32(reader["BookId"]);

                    books.Add(new Books
                    {
                        Title = title,
                        ID = id
                    });
                }
            }

            return books;
        }
    }
}

