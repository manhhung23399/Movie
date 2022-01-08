using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Response
{
    public class GenresResponse 
    {
        public string Id { get; set; }  
        public string Name { get; set; }
        public GenresResponse(Genre genre)
        {
            Id = genre.Id;
            Name = genre.Name;
        }
    }
}
