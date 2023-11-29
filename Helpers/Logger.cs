using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Helpers
{
    public class Logger
    {
        public void log(string Message){
            string Path = Directory.GetCurrentDirectory()+"\\Logger.txt";
            try{

             using (StreamWriter streamWriter = new StreamWriter(Path,append:true))
                {
                    streamWriter.WriteLine("--------------------------------------------------------------------------------------------------------");
                    streamWriter.WriteLine(TimeServer.Now_s());
                    streamWriter.WriteLine(Message);
                    streamWriter.WriteLine("--------------------------------------------------------------------------------------------------------");
                    streamWriter.Close();
                }
            }catch{}
        } 
    }
}