namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa
    {
        public int numero;
        private readonly string cor;
        private readonly string etiqueta;

        public string Cor { get => cor; }
        public string Etiqueta { get => etiqueta; } 

        public Caixa(string cor, string etiqueta)
        {
            this.cor = cor;
            this.etiqueta = etiqueta;
        }
    }
}
