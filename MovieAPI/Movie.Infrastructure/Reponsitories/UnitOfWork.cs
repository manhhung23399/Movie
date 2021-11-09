
using Movie.Core.Interfaces;

namespace Movie.Infrastructure.Reponsitories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAccountReponsitory Account { get; private set; }
        public IMovieReponsitory Movie { get; private set; }
        public IGenresReponsitory Genre { get; private set; }
        public ICompanyReponsitory Company { get; private set; }
        public ICastReponsitory Cast { get; private set; }
        public UnitOfWork(
            IAccountReponsitory account,
            IMovieReponsitory movie,
            IGenresReponsitory genres,
            ICastReponsitory cast,
            ICompanyReponsitory company)
        {
            Account = account;
            Movie = movie;
            Genre = genres;
            Cast = cast;
            Company = company;
        }
    }
}
