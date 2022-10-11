using Api_Project.DAL.Entities;
using Api_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.BL.IRep
{
   public interface ILibraryRep
    {
        public ICollection<LibraryModel> GetAllLibraries(int Page);
        public ICollection<BookModel> GetBooksInLibrary(int LibraryId,int Page);
        public ICollection<BookModel> GetBooksCheckedInLibrary(int LibraryId);
        public string AddLibrary(LibraryModel model);
        public string EditLibrary(int LibraryId, LibraryModel NewOne);
        public string DeleteLibrary(int LibraryId);
    }
}
