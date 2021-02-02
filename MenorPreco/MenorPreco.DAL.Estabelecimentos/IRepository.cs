using MenorPreco.Model;
using System.Linq;

namespace MenorPreco.DAL.Estabelecimentos
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity ProcurarPeloEstabelecimento(int key);

        void IncluirEstabelecimento(Estabelecimento estabelecimento);

        void SalvaProduto();
        void InclueProduto(Produto produto);

        TEntity ProcurarPorProduto(long key);

        void InclueVenda(Venda venda);

        void InclueVendaProduto(VendaProduto vendaProduto);

        IQueryable<TEntity> All { get; }

        IQueryable<Produto> InclueEstabelecimento { get; }
    }
}

