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
        LojaPecaDAO<Peca> peca = new LojaPecaDAO<Peca>();
        LojaPecaDAO<Venda> venda = new LojaPecaDAO<Venda>();
        LojaPecaDAO<VendaPeca> vendaPeca = new LojaPecaDAO<VendaPeca>();
        public Peca CriarPeca(string descricao, double valor)
        {
            var pecaExistente = peca.RecuperarUmPor(p => p.Descricao.Equals(descricao) && p.Valor == valor);
            if (valor == 0) throw new Exception("valor inválido");
            if (pecaExistente is not null)
            {
                throw new Exception("peça já existe no sistema");
            }
            Peca pecaCriada = new Peca(descricao, valor);
            peca.Adicionar(pecaCriada);
            return pecaCriada;
        }

        public void AdicionarPecaVenda(int idVenda, int idPeca, int qtd)
        {
            if (idVenda == 0 || idPeca == 0 || qtd == 0)
            {
                throw new Exception("valor 0");
            }
            var pecaExistente = peca.RecuperarUmPor(p => p.Id == idPeca);
            var vendaExistente = venda.RecuperarUmPor(venda => venda.Id == idVenda);
            if (pecaExistente is null) 
            {
                throw new Exception ("Peça nao existe"); 
            }

            if (vendaExistente is not null) 
            {
                var pecaExistenteVP = vendaPeca.RecuperarUmPor(v => v.Peca.Id == idPeca && v.Venda.Id == idVenda);
                if (vendaExistente.VendaFinalizada || vendaExistente.VendaCancelada)
                {
                    throw new Exception("impossível aleterar");
                }
                if (pecaExistenteVP is not null)
                {
                    pecaExistenteVP.Quantidade += qtd;
                    vendaPeca.Atualizar(pecaExistenteVP);
                }
                else 
                {
                    CriarVendaPeca(pecaExistente, vendaExistente, qtd);                  
                }
            } else
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
            vendaPeca.Atualizar(novaVendaPeca2);
            //vendaPeca.Adicionar(novaVendaPeca2);          
        }
        public void LimparVenda(int idVenda)
        {
            var vendaExistenteVP = vendaPeca.RecuperarUmPor(v => v.Venda.Id == idVenda);
            var pecaDeletar = vendaPeca.RecuperarUmPor(v => v.Venda.Id == idVenda && v.Peca.Id > 0);
            vendaExistenteVP.ValorUnitario = 0;
            vendaExistenteVP.Quantidade = 0;
            if (vendaExistenteVP is not null) 
            {

                vendaPeca.Deletar(pecaDeletar);
                vendaPeca.Atualizar(vendaExistenteVP);
            }
        }
    }
}


