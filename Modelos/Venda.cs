

using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

namespace LojaPeca.Modelos
{
    internal class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotalComDesconto { get; set; }
        public bool VendaFinalizada { get; set; }
        public bool VendaCancelada { get; set; }
    }
}
