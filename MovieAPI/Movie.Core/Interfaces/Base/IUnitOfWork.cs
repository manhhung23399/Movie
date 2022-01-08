using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountReponsitory Account { get; }
        IMovieReponsitory Movie { get; }
        public IGenresReponsitory Genre { get; }
        public ICompanyReponsitory Company { get; }
        public ICastReponsitory Cast { get; }
        public ICommentReponsitory Comment { get; }
    }
}
