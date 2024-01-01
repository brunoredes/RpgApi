using System.ComponentModel.DataAnnotations;

namespace RpgApi.api.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] Foto { get; set; } = null;
    public double? Latitude { get; set; } = null;
    public double? Longitude { get; set; } = null;
    public DateTime DataAcesso { get; set; } = DateTime.Now;
    public List<Personagem> Personagens { get; set; } = null;
    public string Perfil { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

}