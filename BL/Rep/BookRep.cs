using Api_Project.BL.IRep;
using Api_Project.DAL.DataBase;
using Api_Project.DAL.Entities;
using Api_Project.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

                var entity = _mapper.Map<Book>(model);
                _db.Books.Add(entity);
                _db.SaveChanges();
                List<int> list = JsonConvert.DeserializeObject<List<int>>(model.LibraiesFoundIn);
                foreach (var item in list)
                {
                   if( _db.BookLibraries.FirstOrDefault(f=>f.BookId==entity.Id && f.LibraryId == item) == null)
                    {
                        _db.BookLibraries.Add(new BookLibrary() { BookId = entity.Id, LibraryId = item });
                    }
                }
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
            if (book is null || library is null || _db.LibraryBooksChecked.FirstOrDefault(f=>f.UserId == UserId&&f.BookId == BookId&&f.LibraryId == LibraryId&&f.CheckFinished == false)!=null)
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
            _db.BookLibraries.RemoveRange(_db.BookLibraries.Where(w => w.BookId == BookId));

            return _db.SaveChanges()==1? "Good":"The book dont Deleted Yet"; 

        }

        public string EditBook(int BookId, BookModel NewBook)
        {
           
            var OldBook = _db.Books.FirstOrDefault(f => f.Id == BookId);
            var oldbooklibrariesin = _db.BookLibraries.Where(w => w.BookId == BookId).Select(s => s.LibraryId).ToList();
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

                var LibrariesBookIn = JsonConvert.DeserializeObject<List<int>>(NewBook.LibraiesFoundIn);
                if(LibrariesBookIn != null)
                {
                    foreach(var item in LibrariesBookIn)
                    {
                        if (oldbooklibrariesin.Contains(item))
                        {
                            oldbooklibrariesin.Remove(item);
                        }
                        else
                        {
                            _db.BookLibraries.Add(new BookLibrary(){ BookId = BookId,LibraryId = item });
                        }
                    }
                    // delete other libraries 
                    if (oldbooklibrariesin != null)
                    {
                        foreach (var item in oldbooklibrariesin)
                        {
                            _db.BookLibraries.Remove(_db.BookLibraries.FirstOrDefault(f => f.BookId == BookId && f.LibraryId == item));
                        }
                    }

                }
                else
                {
                    //if null remove all libraries to this book 
                    _db.BookLibraries.RemoveRange(_db.BookLibraries.Where(w => w.BookId == BookId));

                }


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


        public BookModel GetBookById(int Id) {

            var entity = _db.Books
                .Include(i=>i.Author)
                .Include(i=>i.BookType)
                .FirstOrDefault(f => f.Id == Id);        
               var model =  _mapper.Map<BookModel>(entity);
            model.AuthorName = entity.Author.Name;
            model.BookTypeName = entity.BookType.Name;
            model.LibraiesFoundIn = JsonConvert.SerializeObject(_db.BookLibraries.Where(w => w.BookId == Id).Select(s => s.LibraryId));
            model.LibrariesContainBook = _db.BookLibraries.Include(i=>i.Library).Where(w => w.BookId == Id).Select(s=>_mapper.Map<LibraryModel>(s.Library) ).ToList();


            return model;

        }
       

        public ICollection<LibraryBookCheckedModel> GetBooksCheckedToUser(string UserId)
        {
            var booksChecked = new List<LibraryBookCheckedModel>();
             foreach(var item in _db.LibraryBooksChecked.Include(i => i.Book) .Include(i => i.Library).Where(w => w.UserId == UserId && w.CheckFinished == false).ToList())
             {
                    var x = _mapper.Map<LibraryBookCheckedModel>(item);
                    x.BookName = item.Book.Name;
                    x.LibraryName = item.Library.Name;
                    booksChecked.Add(x);
             }
            return booksChecked; 
        }

        public ICollection<BookModel> SearchInBooks(string Word) => _mapper.Map <List<BookModel>>( _db.Books.Where(w => w.BookType.Name.Contains(Word) || w.Name.Contains(Word)).ToList());
        
    }
}
