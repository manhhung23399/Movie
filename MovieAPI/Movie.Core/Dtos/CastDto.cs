using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Dtos
{
    public class CastDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? Biography { get; set; }
    }
}
