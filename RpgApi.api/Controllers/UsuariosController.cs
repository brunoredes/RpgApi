using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.api.Data;
using RpgApi.api.Models;
using RpgApi.api.Services;

namespace RpgApi.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> UsuarioExistente(string username)
        {
            return await _context.Usuarios.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetUsuario(int usuarioId)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == usuarioId);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByLogin/{login}")]
        public async Task<IActionResult> GetUsuario(string login)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Username.ToLower() == login.ToLower());

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioInput user)
        {
            try
            {
                HashService hashService = new HashService();
                var salt = hashService.CreateSalt();
                bool usuarioExistente = await UsuarioExistente(user.Username);
                if (usuarioExistente)
                {
                    return BadRequest("Usuário já existe");
                }
                Usuario novoUsuario = new Usuario
                {
                    Username = user.Username,
                    PasswordHash = hashService.HashPassword(user.PasswordString, salt),
                    PasswordSalt = salt
                };

                await _context.Usuarios.AddAsync(novoUsuario);
                await _context.SaveChangesAsync();

                return Created("/Registrar", novoUsuario.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(UsuarioInput credenciais)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));
                var hashService = new HashService();

                if (usuario == null)
                {
                    return NotFound("Usuario não encontrado.");
                }
                if (!hashService.VerifyPassword(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    throw new Exception(usuario.PasswordHash.ToString());
                }
                else
                {
                    usuario.DataAcesso = DateTime.Now;
                    await _context.SaveChangesAsync();

                    return Ok(usuario);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(Usuario credenciais)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));

                if (usuario == null)
                {
                    throw new Exception("Usuario não encontrado.");
                }
                else
                {
                    //Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt);
                    //usuario.PasswordHash = hash;
                    //usuario.PasswordSalt = salt;
                    await _context.SaveChangesAsync();

                    string message = $"Senha do '{usuario.Username}' alterada com sucesso!";
                    return Ok(message);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AtualizarEmail")]
        public async Task<IActionResult> AtualizarEmail(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == u.Id);

                usuario.Email = u.Email;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.Id).IsModified = false;
                attach.Property(x => x.Email).IsModified = true;

                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AtualizarFoto")]
        public async Task<IActionResult> AtualizarFoto(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == u.Id);

                usuario.Foto = u.Foto;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.Id).IsModified = false;
                attach.Property(x => x.Foto).IsModified = true;

                int linhasAfetadas = await _context.SaveChangesAsync(); return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AtualizarLocalizacao")]
        public async Task<IActionResult> AtualizarLocalizacao(Usuario u)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == u.Id);

                usuario.Latitude = u.Latitude;
                usuario.Longitude = u.Longitude;

                var attach = _context.Attach(usuario);
                attach.Property(x => x.Id).IsModified = false;
                attach.Property(x => x.Latitude).IsModified = true;
                attach.Property(x => x.Longitude).IsModified = true;

                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Usuario> lista = await _context.Usuarios.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}