using Microsoft.Extensions.DependencyInjection;
using Movie.Core.Interfaces;
using Movie.Infrastructure.DbContext;
using Movie.Infrastructure.Extensions;
using Movie.Infrastructure.Reponsitories;

namespace Movie.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterDIContainer(this IServiceCollection services, string rootImagePath)
        {
            services.AddSingleton<IFirebaseManager, FirebaseManager>();
            services.AddScoped<IFileEvent>(x => new FileEvent(x.GetService<IFirebaseManager>(), rootImagePath));
            services.AddScoped<IMovieReponsitory, MovieReponsitory>();
            services.AddScoped<IAccountReponsitory, AccountReponsitory>();
            services.AddScoped<IGenresReponsitory, GenresReponsitory>();
            services.AddScoped<ICompanyReponsitory, CompanyReponsitory>();
            services.AddScoped<ICastReponsitory, CastReponsitory>();
            services.AddScoped<ICommentReponsitory, CommentReponsitory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
