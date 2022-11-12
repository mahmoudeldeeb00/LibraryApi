using Api_Project.DAL.DataBase;
using Api_Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Helpers
{
    public class SelectHelper
    {
        private readonly DbContainer _db;
        public SelectHelper(DbContainer db )
        {
            this._db = db;
        }
       
    }
}
