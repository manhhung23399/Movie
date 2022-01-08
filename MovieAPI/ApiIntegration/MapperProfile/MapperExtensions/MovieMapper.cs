using Movie.Core.Interfaces;
using Movie.Core.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.MapperProfile.MapperExtensions
{
    public class MovieMapper 
    {
        private IUnitOfWork _uniOfWork;
        public MovieMapper(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;    
        }
        public async Task<List<Genre>> MapperGenres(string genreIds)
        {
            try
            {
                List<Genre> genres = (List<Genre>)await _uniOfWork.Genre.GetGenresAsync();
                return genres.Where(x => genreIds.Contains(x.Id)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
