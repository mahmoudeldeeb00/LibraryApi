using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Models
{
    public class BookLibraryModel
    {
        [Required(ErrorMessage = "this Book Id  is Required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "this Library id is Required")]
        public int LibraryId { get; set; }
       
    }
}
