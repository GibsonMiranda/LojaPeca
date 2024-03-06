namespace LojaPeca.Modelos
{
    public class VendaPeca
    {
        public int Id { get; set; }
        public virtual Venda Venda { get; set; }
        public virtual Peca Peca { get; set; }
        public double ValorUnitario { get; set; }
        public int Quantidade { get; set; }

        public override string ToString()
        {
            return $"ID VendaPeça: {Id} \n " +
                    $"- ID Venda: {Venda.Id} - " +
                    $"ID Peça: {Peca.Id} - Qtd {Quantidade} - Valor Unitário: R$ {ValorUnitario} - Total: R$ {Quantidade * ValorUnitario}";
        }
    }
}
