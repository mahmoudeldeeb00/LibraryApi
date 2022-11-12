using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Helpers
{
    public static class SaveFileHelper
    {
        public static string SaveAuthorPic(IFormFile pic)
        {
            try
            {
                string folderPath = "D:\\angular demos\\Library demo\\library\\src\\assets\\author pictures";
                string FileName = Guid.NewGuid().ToString() + pic.FileName;
                string FinalPath = Path.Combine(folderPath, FileName);
                using (var stream = new FileStream(FinalPath, FileMode.Create))
                {
                    pic.CopyTo(stream);
                }
                return FileName;
            }
            catch
            {
                return "Default.jpg";
            }



        }
    }
}
