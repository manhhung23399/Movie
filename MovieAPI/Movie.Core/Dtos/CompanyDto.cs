using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Dtos
{
    public class CompanyDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
        public string HomePage { get; set; }
        public string HeadQuarter { get; set; }
        public string Country { get; set; }
    }
}
