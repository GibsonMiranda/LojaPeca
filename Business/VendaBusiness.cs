using LojaPeca.Banco;
using LojaPeca.Modelos;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LojaPeca.Business
{
    internal class VendaBusiness
    {
        LojaPecaDAO<Peca> pecaDAO;
        LojaPecaDAO<Venda> vendaDAO;
        LojaPecaDAO<VendaPeca> vendaPecaDAO;
        public VendaBusiness()
        {
            LojaPecaContext context = new LojaPecaContext();
            pecaDAO = new LojaPecaDAO<Peca>(context);
            vendaDAO = new LojaPecaDAO<Venda>(context);
            vendaPecaDAO = new LojaPecaDAO<VendaPeca>(context);
        }
        public Peca CriarPeca(string descricao, double valor)
        {
            var pecaExistente = pecaDAO.RecuperarUmPor(p => p.Descricao.Equals(descricao) && p.Valor == valor);
            if (valor == 0) throw new Exception("valor inválido");
            if (pecaExistente is not null)
            {
                throw new Exception("peça já existe no sistema");
            }
            Peca pecaCriada = new Peca()
            {
                Descricao = descricao,
                Valor = valor
            };
            pecaDAO.Adicionar(pecaCriada);
            return pecaCriada;
        }

        public void AdicionarPecaVenda(int idVenda, int idPeca, int qtd)
        {
            if (idVenda == 0 || idPeca == 0 || qtd == 0)
            {
                throw new Exception("valor 0");
            }
            var pecaExistente = pecaDAO.RecuperarUmPor(p => p.Id == idPeca);
            var vendaExistente = vendaDAO.RecuperarUmPor(venda => venda.Id == idVenda);
            if (pecaExistente is null)
            {
                throw new Exception("Peça nao existe");
            }

            if (vendaExistente is not null)
            {
                var pecaExistenteVP = vendaPecaDAO.RecuperarUmPor(v => v.Peca.Id == idPeca && v.Venda.Id == idVenda);
                if (vendaExistente.VendaFinalizada || vendaExistente.VendaCancelada)
                {
                    throw new Exception("impossível alterar");
                }
                if (pecaExistenteVP is not null)
                {
                    pecaExistenteVP.Quantidade += qtd;
                    vendaPecaDAO.Atualizar(pecaExistenteVP);
                }
                else
                {
                    CriarVendaPeca(pecaExistente, vendaExistente, qtd);
                }
            }
            else
            {
                Venda venda = new Venda();
                CriarVendaPeca(pecaExistente, venda, qtd);


            }
        }
        private void CriarVendaPeca(Peca peca, Venda venda, int quantidade)
        {
            VendaPeca novaVendaPeca2 = new VendaPeca()
            {
                Peca = peca,
                Venda = venda,
                Quantidade = quantidade,
                ValorUnitario = peca.Valor,
            };
            vendaPecaDAO.Adicionar(novaVendaPeca2);
                     
        }
        public void LimparVenda(int idVenda)
        {
            BuscarVenda(idVenda);
            vendaPecaDAO.DeletarTodos(v => v.Venda.Id.Equals(idVenda));
        }

        public void CancelarVenda(int idVenda)
        {
            var venda = BuscarVenda(idVenda);
            if (venda.VendaCancelada)
            {
                throw new Exception("venda já está cancelada");
            }
            else
            {
                venda.VendaCancelada = true;
                vendaDAO.Atualizar(venda);
            }
        }
        private Venda BuscarVenda(int idVenda)
        {
            if (idVenda == 0)
            {
                throw new Exception("venda inválida");
            }
            return vendaDAO.RecuperarUmPor(v => v.Id == idVenda) ?? throw new Exception("venda inexistente");
        }
        public void CancelarVendaPeca(int idVenda, int idPeca, int qtd)
        {
            var venda = BuscarVenda(idVenda);
            if (venda.VendaCancelada || venda.VendaFinalizada)
            {
                throw new Exception("impossível alterar");
            }
            var pecaExistente = vendaPecaDAO.RecuperarUmPor(v => v.Venda.Id == idVenda && v.Peca.Id == idPeca);
            if (pecaExistente is not null)
            {
                if (pecaExistente.Quantidade >= qtd)
                {
                    pecaExistente.Quantidade -= qtd;
                    vendaPecaDAO.Atualizar(pecaExistente);
                }
                else if (pecaExistente.Quantidade <= qtd)
                {
                    throw new Exception($"quantidade maior que a fornecida {pecaExistente.Quantidade}");
                }
                if (pecaExistente.Quantidade == 0)
                {
                    vendaPecaDAO.Deletar(pecaExistente);
                }
            }
            else
            {
                throw new Exception("peça inexistente");
            }
        }
        public void CancelarTodaPecaVenda(int idVenda, int idPeca)
        {
            var vendaExistente = BuscarVenda(idVenda);
            var pecaExistente = vendaPecaDAO.RecuperarUmPor(v => v.Venda.Id == idVenda && v.Peca.Id == idPeca);

            if (vendaExistente.VendaCancelada || vendaExistente.VendaFinalizada)
            {
                throw new Exception("impossível alterar");
            }
            if (pecaExistente is not null)
            {
                vendaPecaDAO.Deletar(pecaExistente);
            }
        }
        public void FinalizarVenda(int idVenda)
        {
            var vendaExistente = BuscarVenda(idVenda);
            var total = vendaPecaDAO.RecuperarListaPor(v => v.Venda.Id == idVenda).Sum(v => v.Quantidade * v.ValorUnitario);            
            var qtdPecasTotal = vendaPecaDAO.RecuperarListaPor(v => v.Venda.Id == idVenda).Sum(v => v.Quantidade);  
            
            if (vendaExistente.VendaFinalizada  || vendaExistente.VendaCancelada)
            {
                throw new Exception("impossível alterar");
            }
            else 
            {
                Console.WriteLine($" Total Sem Desconto: R$ {total}");
                CalcularDescontoPorCondicao(ref total);                      
                CalcularDescontoQtdPeca(ref total, qtdPecasTotal);
              

            }
            vendaExistente.ValorTotalComDesconto = total;
            vendaExistente.VendaFinalizada = true;
            vendaDAO.Atualizar(vendaExistente);          
            Console.WriteLine(vendaExistente.ToString());
        }

        public void CalcularDesconto(ref double total, double desconto, bool calculoPeca = false, int faixaDescontoPeca = 0)
        {
           
            desconto = desconto / 100;
            total *= 1 - desconto;
            if (calculoPeca)
            {
                Console.WriteLine($"Total da Compra com o desconto de {desconto * 100}% para quantidade de peças acima de {faixaDescontoPeca}: R$ {total}");
            }
            else
            {
                Console.WriteLine($"Total da Compra com o desconto de {desconto * 100}%: R$ {total}");
            }
        }
        public double CalcularDescontoPorCondicao(ref double total)
        {
            if (total > 3000)
            {
                CalcularDesconto(ref total, 15);
                return total;
            }
            else if (total > 1500)
            {
                CalcularDesconto(ref total, 12);
                return total;
            }
            else if (total > 750)
            {
                CalcularDesconto(ref total, 8);
                return total;
            }
            else if (total > 350)
            {
                CalcularDesconto(ref total, 5);
                return total;
            }
            else if (total > 250)
            {
                CalcularDesconto(ref total, 2);
                return total;
            }
            return total;
        }
        public double CalcularDescontoQtdPeca(ref double total , int qtdPecasTotal)
        {
            if (qtdPecasTotal > 200)
            {
                CalcularDesconto(ref total, 10, true, 200);               
                return total;
            }
            else if (qtdPecasTotal > 80)
            {
                CalcularDesconto(ref total, 5);              
                return total;
            }
            else if (qtdPecasTotal > 40)
            {
                CalcularDesconto(ref total, 2);               
                return total;
            }
            return total;
        }
    }
}


