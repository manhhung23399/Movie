using Movie.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.FakerData
{
    public class MovieFakeData
    {
        public async Task FakeDataAsync(string url, string apiKey, Action<MovieModel> action)
        {
            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(apiKey)) throw new Exception("Query is valid");
                if (!url.Contains("api.themoviedb.org")) throw new Exception("Url is valid");
                url += $"?api_key={apiKey}&language=vi&append_to_response=credits";
                HttpClient client = new HttpClient();
                var res = await client.GetAsync(url);
                string data = await res.Content.ReadAsStringAsync();
                var rep = JsonConvert.DeserializeObject<MovieFakeDataFactory>(data);
                rep.results.ForEach(async movieId =>
                {
                    string urlDetail = "https://api.themoviedb.org/3/movie/";
                    urlDetail += $"{movieId.id}?api_key={apiKey}&language=vi&append_to_response=credits";
                    HttpClient client = new HttpClient();
                    var resDetail = await client.GetAsync(urlDetail);
                    string dataDetail = await resDetail.Content.ReadAsStringAsync();
                    var repDetail = JsonConvert.DeserializeObject<MovieData>(dataDetail);
                    if (repDetail != null && !string.IsNullOrEmpty(repDetail.overview))
                    {
                        var movie = new MovieModel
                        {
                            BackDrop = "https://image.tmdb.org/t/p/w500" + repDetail.backdrop_path,
                            BackDropName = "",
                            Id = repDetail.id,
                            DateRelease = DateTime.ParseExact(repDetail.release_date, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture),
                            Description = repDetail.overview,
                            Poster = "https://image.tmdb.org/t/p/w500" + repDetail.poster_path,
                            PosterName = "",
                            Score = repDetail.vote_average,
                            VoteCount = repDetail.vote_count,
                            Title = repDetail.title,
                            Popularity = repDetail.popularity
                        };
                        repDetail.genres.ForEach(g =>
                        {
                            if (string.IsNullOrEmpty(movie.Genres)) movie.Genres += g.id;
                            else
                            {
                                movie.Genres += ",";
                                movie.Genres += g.id;
                            }
                        });
                        repDetail.production_companies.ForEach(com =>
                        {
                            if (string.IsNullOrEmpty(movie.Companies)) movie.Companies += com.id;
                            else
                            {
                                movie.Companies += ",";
                                movie.Companies += com.id;
                            }
                        });
                        repDetail.credits.cast.ForEach(ca =>
                        {
                            if (string.IsNullOrEmpty(movie.Casts)) movie.Casts += ca.id;
                            else
                            {
                                movie.Casts += ",";
                                movie.Casts += ca.id;
                            }
                        });

                        action(movie);
                    }
                });
            }
            catch(Exception ex) { throw ex; }
        }
    }
    public class MovieFakeDataFactory
    {
        public List<MoviePopular> results { get; set; }
    }
    public class MoviePopular
    {
        public string id { get; set; }
    }
    public class MovieData
    {
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
        public List<MovieGenre> genres { get; set; }
        public List<ProductCompany> production_companies { get; set; }
        public CastArray credits { get; set; }
        public string id { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public string overview { get; set; }
        public double popularity { get;set; }
    }

    public class MovieGenre
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class ProductCompany
    {
        public string id { get; set; }
    }
}
