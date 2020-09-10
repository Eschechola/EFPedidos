using System;
using System.Collections.Generic;
using EFPedidos.Data;
using EFPedidos.Domain;
using EFPedidos.ValueObjects;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFPedidos
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoverDados();
            Console.ReadKey();
        }

        private static void RemoverDados()
        {
            using var _context = new EFPedidosContext();

            var cliente = _context.Clientes.Find(5);

            //_context.Remove(cliente);
            //_context.Entry(cliente).State = EntityState.Deleted;

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }


        private static void AtualizarDados()
        {
            using var _context = new EFPedidosContext();
            var cliente = _context.Clientes.Find(5);

            cliente.Nome = "Cliente Alterado";

            //irá ATUALIZAR TODAS AS PROPRIEDADES
            //_context.Update(cliente);

            //_context.Entry(cliente).State = EntityState.Modified;

            // APENAS SALVAR O OBJETO JA ATUALIZA, POIS O EF RASTREIA O OBJETO
            _context.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var _context = new EFPedidosContext();

            //tras o objeto junto com os itens da propriedade de navegação (itens)
            //e depois pega dentro de cada item o produto
            var pedidos = _context
                .Pedidos
                //inclui o primeiro nível (Pedido > Itens)
                .Include(x=> x.Itens)
                //inclui o segundo nível (Item > Produto)
                .ThenInclude(x=>x.Produto).ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void CadastrarPedido()
        {
            using var _context = new EFPedidosContext();

            var cliente = _context.Clientes.FirstOrDefault();
            var produto = _context.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<ItemPedido>()
                {
                    new ItemPedido
                    {
                        ProdutoId= produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = produto.Valor
                    },
                }
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var _context = new EFPedidosContext();
            var consultaPorSintexe = (from cli in _context.Clientes where cli.Id > 0 select cli).ToList();
            var consultaPorMetodo = _context.Clientes.Where(x => x.Id > 0).ToList();
        }


        private static void InserirDadosEmMassa()
        {
            var cliente = new Cliente
            {
                Nome = "Lucas G",
                CEP = "45665432",
                Cidade = "Sampa",
                Estado = "SP",
                Telefone = "11999999999"
            };

            var produto = new Produto
            {
                Descricao = "Processador Ryzen 9 3900X",
                CodigoBarras = "123321321",
                Valor = 4000,
                TipoProduto = TipoProduto.Embalagem,
                Ativo = true
            };

            var listaClientes = new List<Cliente>()
            {
                new Cliente
                {
                    Nome = "Lucas G",
                    CEP = "45665432",
                    Cidade = "Sampa",
                    Estado = "SP",
                    Telefone = "11999999999"
                },

                new Cliente
                {
                    Nome = "Lucas G",
                    CEP = "45665432",
                    Cidade = "Sampa",
                    Estado = "SP",
                    Telefone = "11999999999"
                },

                new Cliente
                {
                    Nome = "Lucas G",
                    CEP = "45665432",
                    Cidade = "Sampa",
                    Estado = "SP",
                    Telefone = "11999999999"
                },
            };

            using var _context = new EFPedidosContext();

            _context.AddRange(produto, cliente);
            _context.AddRange(listaClientes);

            var totalRegistros = _context.SaveChanges();

            Console.WriteLine("Total registros: {0}", totalRegistros);
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Processador Ryzen 9 3900X",
                CodigoBarras = "123321321",
                Valor = 4000,
                TipoProduto = TipoProduto.Embalagem,
                Ativo = true
            };

            // - FORMAS DE INSERIR
            //_context.Set<Produto>().Add(produto);
            //_context.Entry(produto).State = EntityState.Added;
            //_context.Add(produto);

            using var _context = new EFPedidosContext();

            _context.Produtos.Add(produto);
            var totalRegistros = _context.SaveChanges();

            Console.WriteLine("Total registros: {0}", totalRegistros);
        }
    }
}
