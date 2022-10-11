using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Models
{
    public class AuthorModel
    {
      
        [Required(ErrorMessage = "this Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "this BirthDay is Required")]
        public DateTime BirthDay { get; set; }
        public string Details { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
    }
}
