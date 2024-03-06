using LojaPeca.Banco;
using LojaPeca.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
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
                    throw new Exception("impossível aleterar");
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
            //vendaPeca.Adicionar(novaVendaPeca2);          
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
        public void CancelarVendaPeca()
        {
            BuscarVenda();
        }

    }
}


