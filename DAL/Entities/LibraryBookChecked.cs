using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.DAL.Entities
{
    public class LibraryBookChecked
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public LibraryUser LibraryUser { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int LibraryId { get; set; }
        [ForeignKey("LibraryId")]
        public Library Library { get; set; }

        public bool CheckFinished { get; set; } = false;

    }
}
