using Movie.Core.Constants;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace Movie.Infrastructure.Reponsitories
{
    public class CastReponsitory : ICastReponsitory
    {
        private readonly IFirebaseManager _manager;
        private IFileEvent _file;
        private IMapper _mapper;
        public CastReponsitory(
            IFirebaseManager manager,
            IFileEvent file, IMapper mapper)
        {
            _manager = manager;
            _file = file;
            _mapper = mapper;
        }
        public async Task AddOrUpdateCastAsync(Cast cast)
        {
            try
            {
                string path = $"{ArgumentEntities.Cast}/{cast.Id}";
                await _manager.Database().SetAsync(path, cast);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Cast> AddOrUpdateCastAsync(CastDto castDto, string castId = "")
        {
            try
            {
                string path = string.IsNullOrEmpty(castId) ? $"{ArgumentEntities.Cast}/{castDto.Id}" : $"{ArgumentEntities.Cast}/{castId}";
                if (string.IsNullOrEmpty(castId))
                {
                    var checkCastByName = await _manager.Database().GetAsync(ArgumentEntities.Cast);
                    if (checkCastByName.Body != "null")
                    {
                        var data = checkCastByName.ResultAs<Dictionary<string, Cast>>();
                        if (string.IsNullOrEmpty(castId))
                        {
                            if (data.Values.ToList().FirstOrDefault(x => x.Name.Equals(castDto.Name)) != null)
                                throw new Exception(Notify.NOTIFY_ISVALID_MOVIE);
                        }
                    }
                }

                Cast cast = new Cast();
                cast = _mapper.Map<Cast>(castDto);

                if (!string.IsNullOrEmpty(castId)) cast.Id = castId;
                if (castDto.Avatar != null) cast.Avatar = await _file.UploadStreamAsync(castDto.Avatar, ArgumentEntities.Cast);
                await _manager.Database().SetAsync(path, cast);
                return cast;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCastAsync(string castId)
        {
            try
            {
                var cast = (await _manager.Database().GetAsync($"{ArgumentEntities.Company}/{castId}")).ResultAs<Cast>();
                if(!string.IsNullOrEmpty(cast.FileName)) await _manager.Storage().Child(ArgumentEntities.Cast).Child(cast.FileName).DeleteAsync();
                await _manager.Database().DeleteAsync($"{ArgumentEntities.Cast}/{castId}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetCastAsync(string castId = "")
        {
            try
            {
                string path = string.IsNullOrEmpty(castId) ? ArgumentEntities.Cast : $"{ArgumentEntities.Cast}/{castId}";
                var data = await _manager.Database().GetAsync(path);
                if (data.Body == "null") throw new Exception("Cast is not exist");
                if (string.IsNullOrEmpty(castId))
                {
                    var movies = data.ResultAs<Dictionary<string, Cast>>();
                    return movies.Values.ToList();
                }
                else
                {
                    var movie = data.ResultAs<Cast>();
                    return movie;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
