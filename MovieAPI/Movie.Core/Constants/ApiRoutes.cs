using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Constants
{
    public static class ApiRoutes
    {
        private const string ApiRoutesDefault = "api/v1";
        public const string QUERY = "{id}";
        public const string FAKE = "fake";
        public static class Movie
        {
            public const string DEFAULT = ApiRoutesDefault + "/movie";
        }
        public static class Genre
        {
            public const string DEFAULT = ApiRoutesDefault + "/genre";
        }
        public static class Cast
        {
            public const string DEFAULT = ApiRoutesDefault + "/cast";
        }
        public static class Company
        {
            public const string DEFAULT = ApiRoutesDefault + "/company";
        }
    }
}
