using Movie.Core.Constants;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Reponsitories
{
    public class GenresReponsitory : IGenresReponsitory
    {
        private IFirebaseManager _manager;
        public GenresReponsitory(IFirebaseManager manager)
        {
            _manager = manager;
        }

        public async Task<object> GetGenresAsync(string genreId = "")
        {
            try
            {
                if (string.IsNullOrEmpty(genreId))
                {
                    var data = await _manager.Database().GetAsync(ArgumentEntities.Genre);
                    if(data.Body == "null") return new List<Genre>();
                    var convertDataToList = data.ResultAs<Dictionary<string, Genre>>();
                    return convertDataToList.Values.ToList();
                }
                else
                {
                    var data = await _manager.Database().GetAsync($"{ArgumentEntities.Genre}/{genreId}");
                    if(data.Body == "null") throw new Exception(Notify.NOTIFY_VALID_GENRES);
                    var genre = data.ResultAs<Genre>();
                    return genre;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteGenresAsync(string genreId)
        {
            try
            {
                var data = await _manager.Database().GetAsync($"{ArgumentEntities.Genre}/{genreId}");
                if (data.Body == "null") throw new Exception(Notify.NOTIFY_VALID_GENRES);
                await _manager.Database().DeleteAsync($"{ArgumentEntities.Genre}/{genreId}");
            }
            catch(Exception ex) { throw ex; }   
        }

        public async Task AddOrUpdateGenresAsync(Genre genre, string genreId)
        {
            try
            {
                string path = string.IsNullOrEmpty(genreId) ? $"{ArgumentEntities.Genre}/{genre.Id}" : $"{ArgumentEntities.Genre}/{genreId}";
                var checkData = await _manager.Database().GetAsync(ArgumentEntities.Genre);

                if (checkData.Body == "null") throw new Exception(Notify.NOTIFY_ERROR);
                var data = checkData.ResultAs<Dictionary<string, Genre>>();

                if (string.IsNullOrEmpty(genreId))
                {
                    if (data.Values.ToList().FirstOrDefault(x => x.Name == genre.Name) != null)
                        throw new Exception(Notify.NOTIFY_ISVALID_GENRES);
                }
                await _manager.Database().SetAsync(path, genre);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddOrUpdateGenresAsync(Genre genre)
        {
            try
            {
                string path = $"{ArgumentEntities.Genre}/{genre.Id}";
                await _manager.Database().SetAsync(path, genre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
