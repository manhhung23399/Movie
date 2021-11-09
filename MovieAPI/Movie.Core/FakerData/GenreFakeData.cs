using Movie.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.FakerData
{
    public static class GenreFakeByUri
    {
        public const string Uri = "https://api.themoviedb.org/3/genre/movie/list?api_key=8f4464d353e943f2c418c0fd14062822&language=vi&fbclid=IwAR2VkgqQ6aldNdkz8D1SPXaIvKn35XXdfeJ0IAFCpr6lF2OoiuYh5p2BSpQ";
    }
    public class GenreFakeData
    {
        public List<GenreObject> genres { get; set; }
        public async Task FakeAsync(Action<Genre> action)
        {
            HttpClient client = new HttpClient();
            var res = await client.GetAsync(GenreFakeByUri.Uri);
            string data = await res.Content.ReadAsStringAsync();
            var rep = JsonConvert.DeserializeObject<GenreFakeData>(data);
            rep.genres.ForEach(genre =>
            {
                action(new Genre { Id = genre.id, Name = genre.name });
            });
        }
    }
    public class GenreObject
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
