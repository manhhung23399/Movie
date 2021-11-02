using Firebase.Database;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Models.Interfaces
{
    public class FirebaseManager
    {
        protected FirebaseClient database;
        public FirebaseManager(IOptions<AppSettings> options)
        {
            database = new FirebaseClient(options.Value.DatabaseURL);
        }
    }
}
