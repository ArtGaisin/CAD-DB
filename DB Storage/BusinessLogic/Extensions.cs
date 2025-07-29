using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
