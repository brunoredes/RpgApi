namespace RpgApi.api.Services
{
    public interface IHashService
    {
        public byte[] HashPassword(string password, byte[] salt);
        public bool VerifyPassword(string password, byte[] hash, byte[] salt);
        public byte[] CreateSalt();
    }
}
