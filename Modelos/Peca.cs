namespace LojaPeca.Modelos
{
    public class Peca
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public Peca(string descricao, double valor)
        {
            this.Descricao = descricao;
            this.Valor = valor;
        }
        public Peca() { }
    }
}
