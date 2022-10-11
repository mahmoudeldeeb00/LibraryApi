using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Models
{
    public class LibraryBookCheckedModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "this field is Required")]
        public string UserId { get; set; }      
        [Required(ErrorMessage = "this field is Required")]
        public int BookId { get; set; }    
        [Required(ErrorMessage = "this field is Required")]
        public int LibraryId { get; set; }
     
        public bool CheckFinished { get; set; } = false;

    }
}
