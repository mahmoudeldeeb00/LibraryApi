using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.DAL.Entities
{
    public class BookLibrary
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int LibraryId { get; set; }
        [ForeignKey("LibraryId")]
        public Library Library { get; set; }

    }
}
