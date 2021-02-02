using MenorPreco.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MenorPreco.DAL.Estabelecimentos
{
    public class RepositorioBaseEF<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MenorPrecoContext _context;

        public RepositorioBaseEF(MenorPrecoContext context)
        {
            _context = context;
        }

        public TEntity ProcurarPeloEstabelecimento(int key)
        {
            return _context.Find<TEntity>(key);
        }

        public void IncluirEstabelecimento(Estabelecimento estabelecimento) => _context.Set<Estabelecimento>().Add(estabelecimento);

        public void SalvaProduto() => _context.SaveChanges();
        public void InclueProduto(Produto produto) => _context.Set<Produto>().Add(produto);

        public TEntity ProcurarPorProduto(long key)
        {
            return _context.Find<TEntity>(key);
        }

        public void InclueVenda(Venda venda) => _context.Set<Venda>().Add(venda);

        public void InclueVendaProduto(VendaProduto vendaProduto) => _context.Set<VendaProduto>().Add(vendaProduto);

        public IQueryable<TEntity> All => _context.Set<TEntity>().AsQueryable();

        public IQueryable<Produto> InclueEstabelecimento => _context.Set<Produto>().Include(x => x.ESTABELECIMENTO);
    }
}
