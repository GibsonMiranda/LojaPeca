namespace LojaPeca.Modelos
{
    internal class VendaPeca
    {
        public Venda? Venda { get; set; }
        public Peca? Peca { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
