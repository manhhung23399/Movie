using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Interfaces
{
    public interface ICommentReponsitory
    {
        Task<Comment> AddCommentAsync(Comment comment, string movieId);
        Task<IEnumerable<Comment>> GetCommentAsync(string movieId);
    }
}
