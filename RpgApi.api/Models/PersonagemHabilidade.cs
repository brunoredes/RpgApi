namespace RpgApi.api.Models
{
    public class PersonagemHabilidade
    {
        public int PersonagemId { get; set; }
        public Personagem Personagem { get; set; }
        public int HabilidadeId { get; set; }
        public Habilidade Habilidade { get; set; }
    }
}