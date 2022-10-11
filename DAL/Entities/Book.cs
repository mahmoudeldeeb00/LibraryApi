using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
        public int PageNumbers { get; set; }



        public int? BookTypeId { get; set; }
        [ForeignKey("BookTypeId")]
        public BookType BookType { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public ICollection<Library> Libraries { get; set; }


    }
}
