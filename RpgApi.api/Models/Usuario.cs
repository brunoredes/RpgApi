namespace RpgApi.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[]? Foto { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public DateTime? DataAcesso { get; set; } //using System;
    public List<Personagem> Personagens { get; set; }
    public string Perfil { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}