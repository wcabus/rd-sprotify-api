using Sprotify.Domain.Services;
using Sprotify.Application.Albums;
using Sprotify.Application.Artists;
using Sprotify.Application.Songs;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Bootstrap
    {
        public static IServiceCollection AddSprotifyServices(this IServiceCollection services)
        {
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ISongService, SongService>();

            return services;
        }
    }
}
