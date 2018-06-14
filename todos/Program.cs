using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace todos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            //var db = new DBinterface();
            //var data = db.Get("SELECT * FROM user");
            //var data = Utility.Instance.MyQueryString("listusers");
  
            //System.Diagnostics.Trace.WriteLine("johnny " + data);

        }




        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

       
    }

    internal class ex
    {
    }
}
