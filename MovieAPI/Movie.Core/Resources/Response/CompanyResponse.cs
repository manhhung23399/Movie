using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Response
{
    public class CompanyResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public string home_page { get; set; }
        public string head_quarter { get; set; }
        public string country { get; set; }
        public CompanyResponse(Company company)
        {
            id = company.Id;
            name = company.Name;
            logo = company.Logo;
            home_page = company.HomePage;
            head_quarter = company.HeadQuarter;
            country = company.Country;
        }
    }
}
