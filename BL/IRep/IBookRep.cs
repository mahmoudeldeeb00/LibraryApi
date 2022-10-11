using Api_Project.DAL.Entities;
using Api_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.BL.IRep
{
    public interface IBookRep
    {

        public ICollection<BookModel> GetAllBooks(int page);
        public string AddNewBook(BookModel model);
        public BookModel GetBookById(int Id);
        public ICollection<BookModel> SearchInBooks(string Word);
        public ICollection<BookModel> GetBooksCheckedToUser(string UserId);
        public string EditBook(int BookId, BookModel NewBook);
        public string DeleteBook(int BookId);
        public int CheckBook(string UserId , int BookId , int LibraryId);
        public string FinishTheCheck(int CheckId);






    }
}
