using Api_Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Models
{
    public class LibraryModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "this Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "this Adress is Required")]
        public string Address { get; set; }

        [Required]
       
        public int CityId { get; set; }
        public string CityName { get; set; }

        public ICollection<BookModel> Books { get; set; }


    }
}
