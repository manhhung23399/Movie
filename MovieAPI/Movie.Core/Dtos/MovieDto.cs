using Microsoft.AspNetCore.Http;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;

namespace Movie.Core.Dtos
{
    public class MovieDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Poster { get; set; }
        public IFormFile? BackDrop { get; set; }
        public int Score { get; set; }
        public DateTime DateRelease { get; set; } 
        public string? Genres { get; set; }
        public string? Casts { get; set; }
        public string? Companies { get; set; }
    }
}
