using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.DAL.Entities
{
    public class Library
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
     
        public string Address { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        public ICollection<Book> Books { get; set; }


    }
}
