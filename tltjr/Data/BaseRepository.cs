using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace tltjr.Data
{
    public class BaseRepository
    {
        protected MongoDatabase Database { get; private set; }

        protected BaseRepository()
        {
	        //string connection = ConfigurationManager.AppSettings["env"].Equals("local") ? "mongodb://localhost/Users" : ConnectionString.MongoLab;
            var connection = ConfigurationManager.AppSettings.Get("MONGOLAB_URI"); 
            MongoClient client = new MongoClient(connection);
            MongoServer server = client.GetServer();
            var username = ConfigurationManager.AppSettings.Get("mongo_username");
            var password = ConfigurationManager.AppSettings.Get("mongo_password");
            Database = server.GetDatabase("master", new MongoCredentials(username, password));
        }
    }
}