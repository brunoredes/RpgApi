using RpgApi.Services.Usuarios;

namespace RpgApi.Config
{
    public static class ScopedServicesList
    {
        public static void RegisterScopedServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsuariosService, UsuarioService>();
        }
    }
}
