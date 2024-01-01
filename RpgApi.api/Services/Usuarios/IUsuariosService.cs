using RpgApi.api.Models;

namespace RpgApi.api.Services.Usuarios
{
    public interface IUsuariosService
    {
        public Task<Usuario> Registrar(UsuarioInput usuario);
    }
}
