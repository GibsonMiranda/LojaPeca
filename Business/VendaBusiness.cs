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
            
            var qtdPeca1 = vendaPecaDAO.RecuperarListaPor(v => v.Venda.Id == idVenda && v.Quantidade > 200);
            var qtdPeca2 = vendaPecaDAO.RecuperarListaPor(v => v.Venda.Id == idVenda && v.Quantidade > 80);
            var qtdPeca3 = vendaPecaDAO.RecuperarListaPor(v => v.Venda.Id == idVenda && v.Quantidade > 40);
            if (!vendaExistente.VendaFinalizada)
            {               
                Console.WriteLine($" Total Sem Desconto: R$ {total}");
                if (total > 3000)
                {
                    total *= 0.85;
                    Console.WriteLine($"Desconto aplicado no valor de 15% - Total da Compra: R$ {total}");
                } 
                else if (total > 1500) 
                {
                    total *= 0.88;
                    Console.WriteLine($"Desconto aplicado no valor de 12% - Total da Compra: R$ {total}");
                } 
                else if (total > 750)
                {
                    total *= 0.92;
                    Console.WriteLine($"Desconto aplicado no valor de 8% - Total da Compra: R$ {total}");
                }
                else if (total > 350)
                {
                    total *= 0.95;
                    Console.WriteLine($"Desconto aplicado no valor de 5% - Total da Compra: R$ {total}");
                } 
                else if (total > 250)
                {
                    total *= 0.98;
                    Console.WriteLine($"Desconto aplicado no valor de 2% - Total da Compra: R$ {total}");
                }
                if (qtdPeca1 is not null) 
                {
                    total *= 0.90;
                    Console.WriteLine($"Desconto extra de 10% para quantidade acima de 200. Total: {total}");
                }
                else if (qtdPeca2 is not null)
                {
                    total *= 0.95;
                    Console.WriteLine($"Desconto extra de 5% para quantidade acima de 80. Total: {total}");
                }
                else if (qtdPeca3 is not null)
                {
                    total *= 0.98;
                    Console.WriteLine($"Desconto extra de 2% para quantidade acima de 40. Total: {total}");
                }
                Console.WriteLine($"Total da Compra: R$ {total}");
            }
            else 
            {
                throw new Exception("impossível alterar");
            }
            vendaExistente.ValorTotalComDesconto = total;
            vendaExistente.VendaFinalizada = true;
            vendaDAO.Atualizar(vendaExistente);
            vendaPecaDAO.DeletarTodos(v => v.Venda.Id == idVenda);
            Console.WriteLine(vendaExistente.ToString());
        }
    }
}


