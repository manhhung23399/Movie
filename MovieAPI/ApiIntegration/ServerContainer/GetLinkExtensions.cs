using Movie.Core.Entities;
using Movie.Core.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Movie.ApiIntegration.ServerContainer
{
    public static class GetLinkExtensions
    {
        /// <summary>
        /// Get link movie from movie
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static SourceVms GetLink(this Sources sources)
        {
            try
            {
                if(sources == null || string.IsNullOrEmpty(sources.mPhimMoi)) return new SourceVms();
                var client = new HttpClient();
                Uri uri = new Uri("https://mphimmoiitv.com/ajax/player");

                var payload = sources.mPhimMoi.Split(";").ToArray();
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", payload[0]),
                    new KeyValuePair<string, string>("ep", payload[1])
                });

                var req = client.PostAsync(uri, formContent).GetAwaiter().GetResult();
                string rep = req.Content.ReadAsStringAsync().Result;

                Regex regx = new Regex("https://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;]*)?",
                        RegexOptions.IgnoreCase);
                MatchCollection match = regx.Matches(rep);

                string file = match[0].Value.ToString();
                return new SourceVms("hls", file, "hls");
            }
            catch
            {
                return new SourceVms();
            }
        }
    }
}
