using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class Extentions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddDbContext<AppContext>(x =>
            {
                x.UseNpgsql("Host=localhost;Database=NoteDB;Username=postgres;Password=1234");
            });
            return services;
        }
    }
}
