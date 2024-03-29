﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace ActionManager.DAL.Data
{
    public class Startup
    {
        public static string GetConnectionString(int type)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("D:\\University\\Software_semester_2\\ActionManager.DAL\\Data\\appsettings.json");
            var configuration = builder.Build();
            string database = "";
            if (type == 1)
            {
                database = "BloggingDatabase";
            }
            else if (type == 0)
            {
                database = "TestDatabase";
            }
            var connString = configuration.GetConnectionString(database);
            return connString;
        }
    }
}
