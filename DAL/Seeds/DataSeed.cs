using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api_Project.DAL.DataBase;
using Api_Project.DAL.Entities;
using Api_Project.Helpers;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Newtonsoft.Json;

namespace Api_Project.DAL.Seeds
{
    public class DataSeed
    {
        private readonly DbContainer db ;
        private readonly Logger lg ;   
        public DataSeed(DbContainer _db,Logger lg )
        {
            this.db = _db;
            this.lg = lg;
            
        }

        public void Seed(){
            string File = Directory.GetCurrentDirectory()+"\\Files\\";
            if(!db.Countries.Any()){
                try{
                        using StreamReader r = new StreamReader(File+"CountrySeed.json");
                        var json = r.ReadToEnd();
                        List<Country> entities = JsonConvert.DeserializeObject<List<Country>>(json);
                            db.Countries.AddRange(entities);
                            db.SaveChanges();
                       }
                catch(Exception ex)   {lg.log(ex.Message + "\n inner:" + ex.InnerException.Message) ;}
            }
            if(!db.Cities.Any()){
                try{
                        using StreamReader r = new StreamReader(File+"CitySeed.json");
                        var json = r.ReadToEnd();
                        List<City> entities = JsonConvert.DeserializeObject<List<City>>(json);
                            db.Cities.AddRange(entities);
                            db.SaveChanges();
                        }
                catch(Exception ex)   {lg.log(ex.Message + "\n inner:" + ex.InnerException.Message) ;}
            
            }
            if(!db.BookTypes.Any()){
                try{
                        using StreamReader r = new StreamReader(File+"BookType.json");
                        var json = r.ReadToEnd();
                        List<BookType> entities = JsonConvert.DeserializeObject<List<BookType>>(json);
                            db.BookTypes.AddRange(entities);
                            db.SaveChanges();
                        }
                catch(Exception ex)   {lg.log(ex.Message + "\n inner:" + ex.InnerException.Message) ;}
            
            }
        }
    }
}