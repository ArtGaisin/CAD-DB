using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Extensions
    {
        public static IServiceCollection AddBuisnessLogic(this IServiceCollection services)
        {
            services.AddScoped<INoteService, NoteService>();
            return services;
        }
    }
}
