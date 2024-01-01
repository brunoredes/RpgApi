using Microsoft.EntityFrameworkCore;
using RpgApi.api.Data;
using RpgApi.api.Models;

namespace RpgApi.api.Services.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        private readonly DataContext _dataContext;
        private readonly IHashService _hashService;

        public UsuariosService(DataContext dataContext, IHashService hashService)
        {
            _hashService = hashService;
            _dataContext = dataContext;
        }
        public async Task<Usuario> Registrar(UsuarioInput user)
        {
            HashService hashService = new HashService();
            var salt = hashService.CreateSalt();
            bool usuarioExistente = await UsuarioExistente(user.Username);
            if (usuarioExistente)
            {
                throw new Exception("Usuario já existe");
            }
            Usuario novoUsuario = new Usuario
            {
                Username = user.Username,
                PasswordHash = _hashService.HashPassword(user.PasswordString, salt),
                PasswordSalt = salt
            };

            await _dataContext.Usuarios.AddAsync(novoUsuario);
            await _dataContext.SaveChangesAsync();

            return novoUsuario;
        }

        private async Task<bool> UsuarioExistente(string username)
        {
            return await _dataContext.Usuarios.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
    }
}
