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
    public class CommentReponsitory : ICommentReponsitory
    {
        private IFirebaseManager _manager;
        public CommentReponsitory(IFirebaseManager manager)
        {
            _manager = manager;
        }
        public async Task<Comment> AddCommentAsync(Comment comment, string movieId)
        {
            try
            {
                int count = 1;
                var data = await _manager.Database().GetAsync(ArgumentEntities.Comment + "/" + movieId);
                if (data.Body != "null") count += data.ResultAs<Dictionary<string, Comment>>().Count;
                comment.Key = "comment-" + count;
                await Task.Run(() => _manager.Database().SetAsync($"{ArgumentEntities.Comment}/{movieId}/{comment.Key}", comment));
                return comment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentAsync(string movieId)
        {
            try
            {
                var data = await _manager.Database().GetAsync($"{ArgumentEntities.Comment}/{movieId}");
                return data.ResultAs<Dictionary<string, Comment>>().Values.ToList().DefaultIfEmpty(new Comment());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
