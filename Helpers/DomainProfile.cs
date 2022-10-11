using Api_Project.DAL.Entities;
using Api_Project.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Helpers
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<Author, AuthorModel>();
            CreateMap<AuthorModel, Author>();

            CreateMap<Book, BookModel>();
            CreateMap<BookModel, Book>();

            CreateMap<BookLibrary, BookLibraryModel>();
            CreateMap<BookLibraryModel, BookLibrary>();

            CreateMap<Library, LibraryModel>();
            CreateMap<LibraryModel,Library>();
            
            CreateMap<LibraryBookChecked, LibraryBookCheckedModel>();
            CreateMap<LibraryBookCheckedModel, LibraryBookChecked>();


        }
    }
}
