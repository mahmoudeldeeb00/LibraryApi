using Api_Project.BL.IRep;
using Api_Project.DAL.DataBase;
using Api_Project.DAL.Entities;
using Api_Project.Helpers;
using Api_Project.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.BL.Rep
{
    public class AuthorRep : IAuthorRep
    {
        private readonly DbContainer _db;
        private readonly IMapper _mapper;

        public AuthorRep( DbContainer db ,IMapper mapper)
        {
            _db = db;
            this._mapper = mapper;
        }
        public string AddAuthor(AuthorModel model)
        {
            try
            {
                var author = _mapper.Map<Author>(model);
              
                _db.Authors.Add(author);

                _db.SaveChanges();
                return "Good";
            }
            catch
            {
                return "Author not saved in data Base";
            }
           
       
        }

        public string DeleteAuthor(int AuthorId)
        {
            var author = _db.Authors.FirstOrDefault(f => f.Id == AuthorId);
            if (author is null)
                return "Author not found in data base to delete ";          
                _db.Authors.Remove(author);
           return _db.SaveChanges()==1 ? "Good":"Author not saved in data base";

        }

        public string EditAuthor(int AuthorId,AuthorModel NewAuthor)
        {
            var OldAuthor = _db.Authors.FirstOrDefault(f => f.Id == AuthorId);
            if (OldAuthor is null)
                return "Author not found in data base to delete ";
            try
            {
                OldAuthor.BirthDay = NewAuthor.BirthDay;
                OldAuthor.CityId = NewAuthor.CityId;
                OldAuthor.Details = NewAuthor.Details;
                OldAuthor.Name = NewAuthor.Name;

                while(NewAuthor.PictureSrc!=null && NewAuthor.PictureSrc!=OldAuthor.PictureSrc){
                    SaveFileHelper.DeleteOldAuthorPic(OldAuthor.PictureSrc);
                    OldAuthor.PictureSrc = NewAuthor.PictureSrc;

                }
                _db.Authors.Update(OldAuthor);
                _db.SaveChanges();
                return "Good";
            }
            catch
            {
                return "User Not Edited In data base might any foreign key or other data is null ";
            }
        }

        public ICollection<AuthorModel> GetAllAuthors()
        {
            var authorList = _mapper.Map<List<AuthorModel>>(_db.Authors.ToList());
            foreach (var item in authorList)
            {

                item.CityName = _db.Cities.Where(w => w.Id == item.CityId).Select(s => s.Name).FirstOrDefault();
                if (item.PictureSrc is null)
                    item.PictureSrc = "Default.jpg";
            }
            return authorList;
        }


        public AuthorModel GetAuthorById(int AuthorId) {
            var entity = _db.Authors.Include(i => i.City).FirstOrDefault(f => f.Id == AuthorId);
            var model = _mapper.Map<AuthorModel>(entity);
            model.CityName = entity.City.Name;
            model.Books = GetBooksToAuthor((int)model.Id);
            return model;
        }

        public ICollection<BookModel> GetBooksToAuthor(int AuhtorId) {
            var booksList = new List<BookModel>();

            foreach (var item in _db.Books
                .Include(i=> i.Author)
                .Include(i=> i.BookType)
                .Where(w => w.AuthorId == AuhtorId).ToList()
                ) {
                var model = _mapper.Map<BookModel>(item);
                model.AuthorName = item.Author.Name;
                model.BookTypeName = item.BookType.Name;
                booksList.Add(model);
            }
            return booksList;
        }
        public List<City> cities() => _db.Cities.ToList();
    }

}
