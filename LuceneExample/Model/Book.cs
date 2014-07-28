using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneExample
{
    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public List<string> Tags { get; set; }


    }
}
