using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.DAL.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="this Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "this BirthDay is Required")]

        public DateTime BirthDay { get; set; }

        public string Details { get; set; }


      
        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }


        public ICollection<Book> Books { get; set; }
    }
}
