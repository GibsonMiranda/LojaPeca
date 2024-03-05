

using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

namespace LojaPeca.Modelos
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double ValorTotalComDesconto { get; set; }
        public bool VendaFinalizada { get; set; }
        public bool VendaCancelada { get; set; }
    }
}
