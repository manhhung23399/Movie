using Movie.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.FakerData
{
    public class CastFakeData
    {
        public async Task FakeAsync(string url, Action<Cast> action)
        {
            if (!url.Contains("api.themoviedb.org")) throw new Exception("Url is valid");
            HttpClient client = new HttpClient();
            var res = await client.GetAsync(url);
            string data = await res.Content.ReadAsStringAsync();
            var rep = JsonConvert.DeserializeObject<CastCredit>(data);
            rep.credits.cast.ForEach(cast =>
            {
                if(cast.known_for_department.Equals("Acting", StringComparison.InvariantCultureIgnoreCase))
                {
                    action(new Cast
                           {
                               Id = cast.id,
                               Name = cast.name,
                               Avatar = "https://image.tmdb.org/t/p/w500" + cast.profile_path,
                               Biography = "",
                               FileName = ""
                           });
                }
            });
        }
    }

    public class CastCredit
    {
        public CastArray credits { get; set; }
    }
    public class CastArray
    {
        public List<CastDetail> cast { get; set; }
    }
    public class CastDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public string profile_path { get; set; }
        public string known_for_department { get; set; }
    }
}
