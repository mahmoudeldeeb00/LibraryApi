using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api_Project.DAL.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
