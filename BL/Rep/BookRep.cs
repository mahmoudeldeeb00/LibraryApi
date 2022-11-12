using Api_Project.BL.IRep;
using Api_Project.DAL.DataBase;
using Api_Project.DAL.Entities;
using Api_Project.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.BL.Rep
{
    public class BookRep : IBookRep
    {
        private readonly DbContainer _db;
        private readonly IMapper _mapper; 
        public BookRep(DbContainer db , IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public string AddNewBook(BookModel model)
        {
            try
            {
                _db.Books.Add(_mapper.Map<Book>(model));
                _db.SaveChanges();
                return "Good";
            }
            catch
            {
                return "the Book dont added in Data base because data error  might  foreign Keys or others";
            }
           


        }

        public int CheckBook(string UserId, int BookId, int LibraryId)
        {
            var book = _db.Books.FirstOrDefault(f => f.Id == BookId);
            var library = _db.Libraries.FirstOrDefault(f => f.Id == LibraryId);
            if (book is null || library is null)
                return 0;
            try
            {
                var check = new LibraryBookChecked { UserId = UserId, BookId = BookId, LibraryId = LibraryId, CheckFinished = false };
                _db.LibraryBooksChecked.Add(check);
                _db.SaveChanges();
                return check.Id;
            }
            catch
            {
                return 0;
            }
            
               
           
           
        }
        public string FinishTheCheck(int CheckId)
        {
           var check = _db.LibraryBooksChecked.FirstOrDefault(f => f.Id == CheckId);
            if (check is null)
                return "this check is not found ";
            try
            {
                check.CheckFinished = true;
                _db.LibraryBooksChecked.Update(check);
                _db.SaveChanges();
                return "Good";
            }
            catch
            {
                return "the Check dont Finished Yet might foreign keys not founds ";
            }
            
           
        }

        public string DeleteBook(int BookId)
        {
            var book = _db.Books.FirstOrDefault(f => f.Id == BookId);
            if (book is null)
                return "there is no books in data base with this id ";            
              _db.Books.Remove(book);
           return  _db.SaveChanges()==1? "Good":"The book dont Deleted Yet"; 

        }

        public string EditBook(int BookId, BookModel NewBook)
        {
           
            var OldBook = _db.Books.FirstOrDefault(f => f.Id == BookId);
            if (OldBook is null)
                return "there is no books in data base with this id ";
            try
            {
                OldBook.AuthorId = NewBook.AuthorId;
                OldBook.Name = NewBook.Name;
                OldBook.PageNumbers = NewBook.PageNumbers;
                OldBook.Price = NewBook.Price;
                OldBook.PublishDate = NewBook.PublishDate;
                OldBook.BookTypeId = NewBook.BookTypeId;

                _db.Books.Update(OldBook);
                _db.SaveChanges();
                return "Good";
            }
            catch
            {
                return  "The book dont Edited Yet cause error when saving it might foreign keys";
            }
           
        }

        public ICollection<BookModel> GetAllBooks()
        {
            var list = new List<BookModel>();

            foreach (var item in _db.Books.Include(a=>a.Author).Include(b=>b.BookType).ToList())
            {
                var model = _mapper.Map<BookModel>(item);
                model.AuthorName = item.Author.Name;
                model.BookTypeName = item.BookType.Name;
                list.Add(model);

            };
            return list;
        }
       

        public BookModel GetBookById(int Id) => _mapper.Map<BookModel>(_db.Books.FirstOrDefault(f => f.Id == Id));
       

        public ICollection<BookModel> GetBooksCheckedToUser(string UserId)
        {
            var booksChecked = _mapper.Map<List<BookModel>>(_db.LibraryBooksChecked.Where(w => w.UserId == UserId && w.CheckFinished == false).Select(s => s.Book).ToList());
            return booksChecked; 
        }

        public ICollection<BookModel> SearchInBooks(string Word) => _mapper.Map <List<BookModel>>( _db.Books.Where(w => w.BookType.Name.Contains(Word) || w.Name.Contains(Word)).ToList());
        
    }
}
