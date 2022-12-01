using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Models
{
    public class AuthorModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "this Name is Required")]
        public string Name { get; set; }

        public DateTime BirthDay { get; set; }
        public string Details { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string PictureSrc { get; set; }
        public ICollection<BookModel> Books { get; set; }

    

    }
}
