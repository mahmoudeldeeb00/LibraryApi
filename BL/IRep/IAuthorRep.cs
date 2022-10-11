using Api_Project.DAL.Entities;
using Api_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.BL.IRep
{
   public interface IAuthorRep
    {
        public string AddAuthor(AuthorModel model);
        public ICollection<AuthorModel> GetAllAuthors(int page);
        public string EditAuthor(int AuthorId, AuthorModel NewAuthor);
        public string DeleteAuthor(int AuthorId);
        public AuthorModel GetAuthorById(int AuthorId);
        public ICollection<BookModel> GetBooksToAuthor(int AuhtorId);

    }
}
