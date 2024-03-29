﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Models
{
    public class BookModel
    {
        
        public int? Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
        public int PageNumbers { get; set; }
        [Required(ErrorMessage = "Book Type is Required")]
        public int? BookTypeId { get; set; }
      
        public string BookTypeName { get; set; }
        [Required(ErrorMessage = "Author is Required")]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string LibraiesFoundIn { get; set; }
        public List<LibraryModel> LibrariesContainBook { get; set; }


    }
}
