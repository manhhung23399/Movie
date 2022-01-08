using Movie.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.FakerData
{
    public class CompanyFakeData
    {
        private readonly string _urlGetData;
        public CompanyFakeData(string url)
        {
            if(url.Contains("api.themoviedb.org"))
                _urlGetData = url;
        }
        public async Task FakeDataAsync(Action<Company> action)
        {
            try
            {
                if (!string.IsNullOrEmpty(_urlGetData))
                {
                    HttpClient client = new HttpClient();
                    var req = await client.GetAsync(_urlGetData);
                    var resp = await req.Content.ReadAsStringAsync();
                    CompanyGetData data = JsonConvert.DeserializeObject<CompanyGetData>(resp);
                    data.production_companies.ForEach(async company =>
                    {
                        string urlDetail = "https://api.themoviedb.org/3/company/" + company.id + "?api_key=8f4464d353e943f2c418c0fd14062822&fbclid=IwAR0JDQIddL4NM0Oxo50MKTHQ1ri8MAAyKNUi6WC8jq5aiTG7C7EvuNCbY38";
                        req = await client.GetAsync(urlDetail);
                        resp = await req.Content.ReadAsStringAsync();
                        CompanyDetail dataDetail = JsonConvert.DeserializeObject<CompanyDetail>(resp);
                        var item = new Company
                        {
                            Id = company.id,
                            Country = dataDetail.origin_country,
                            HeadQuarter = dataDetail.headquarters,
                            HomePage = dataDetail.homepage,
                            Logo = "https://image.tmdb.org/t/p/w500" + dataDetail.logo_path,
                            Name = dataDetail.name
                        };
                        action(item);
                    });
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
    public class CompanyGetData
    {
        public List<CompanyDetail> production_companies { get; set; }
    }
    public class CompanyDetail
    {
        public string description { get; set; }
        public string id { get; set; }
        public string homepage { get; set; }
        public string headquarters { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
        public string parent_company { get; set; }
    }
}
