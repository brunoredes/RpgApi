using RpgApi.api.Services;
using RpgApi.api.Services.Usuarios;

namespace RpgApi.api.Config
{
    public static class ScopedServicesList
    {
        public static void RegisterScopedServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IHashService, HashService>();
            builder.Services.AddScoped<IUsuariosService, UsuariosService>();
        }
    }
}
