using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPractice.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }

        public Book()
        {
            BookAuthors = new();
        }
    }
}
