using DAL.VideoGamesTest.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.DAL.MySLQ
{
    public static class Registrator
    {
        public static void UseSqlServer(this IServiceCollection services,string connection_string)
        {
            if (connection_string is null)
            {
                //logger
                return;
            }
            services.AddDbContext<GamesContext>(opt => opt.UseSqlServer(connection_string, o => o.MigrationsAssembly(typeof(Registrator).Assembly.FullName)));
        }
    }
}