using Firebase.Storage;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Extensions.Options;
using Movie.Core;
using Movie.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Infrastructure.DbContext
{
    public class FirebaseManager : IFirebaseManager
    {
        protected AppSettings _appSettings;
        public FirebaseManager(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        /// <summary>
        /// Firebase Database Realtime
        /// </summary>
        /// <returns></returns>
        public IFirebaseClient Database()
        {
            IFirebaseConfig database = new FirebaseConfig
            {
                AuthSecret = _appSettings.AuthSecret,
                BasePath = _appSettings.DatabaseURL
            };

            return new FirebaseClient(database);
        }
        /// <summary>
        /// Storage Firebase
        /// </summary>
        /// <returns></returns>
        public FirebaseStorage Storage()
        {
            return new FirebaseStorage(this._appSettings.StorageBucket);
        }
        /// <summary>
        /// Authentication Firebase
        /// </summary>
        /// <returns></returns>
        public Firebase.Auth.FirebaseAuthProvider Authentication()
        {
            var authConfig = new Firebase.Auth.FirebaseConfig(_appSettings.ApiKey);

            Firebase.Auth.FirebaseAuthProvider authProvider = new Firebase.Auth.FirebaseAuthProvider(authConfig);
            return authProvider;
        }
    }
}
