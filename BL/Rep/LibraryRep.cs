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
    public class LibraryRep : ILibraryRep
    {
        private readonly DbContainer _db;
        private readonly IMapper _mapper;

        public LibraryRep( DbContainer db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }
        public  string AddLibrary(LibraryModel model)
        {
            try
            {
                _db.Libraries.Add(_mapper.Map<Library>(model));
                _db.SaveChanges();
                return "Good";
            }
            catch
            {
                return "User Not Edited In data base might any foreign key or other data is null";
            }
                 
          
        }

        public string DeleteLibrary(int LibraryId)
        {
            var lib = _db.Libraries.FirstOrDefault(f => f.Id == LibraryId);
            if (lib is null)
                return "Bad Rquest : there is No Library in data base with this Id";
            _db.Libraries.Remove(lib);
            _db.SaveChanges();
            return "Good";
        }

        public string EditLibrary(int LibraryId, LibraryModel NewOne)
        {
            var OldLibrary = _db.Libraries.FirstOrDefault(f => f.Id == LibraryId);
            if (OldLibrary is null)
                return "there is no libarry in data base with this Id";
            try
            {
                OldLibrary.Name = NewOne.Name;
                OldLibrary.CityId = NewOne.CityId;
                OldLibrary.Address = NewOne.Address;
                _db.Libraries.Update(OldLibrary);
                _db.SaveChanges();
                return "Good";
            }
            catch
            {
                return "TheUser Not Edited In data base might any foreign key or other data is null";
            }
          
        }

        public ICollection<LibraryModel> GetAllLibraries(int Page) {
           var result =  _mapper.Map<List<LibraryModel>>(_db.Libraries.ToList().Skip(--Page*50).Take(50));

            foreach(var item in result)
            {
                var booklist = _db.BookLibraries.Include(i => i.Book).Where(w => w.LibraryId == item.Id).Select(s=>s.Book).ToList();
                item.Books = _mapper.Map<List<BookModel>>(booklist);
            }
            return result;
        } 


        public ICollection<BookModel> GetBooksCheckedInLibrary(int LibraryId) =>_mapper.Map<List<BookModel>>(_db.LibraryBooksChecked.Where(w => w.LibraryId == LibraryId && w.CheckFinished == false).Select(s => s.Book).ToList());

        public ICollection<BookModel> GetBooksInLibrary(int LibraryId,int Page) => _mapper.Map<List<BookModel>>(_db.BookLibraries.Where(w => w.LibraryId == LibraryId).Select(s => s.Book).ToList().Skip(--Page*5).Take(5));
       
    }
}
