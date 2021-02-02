namespace MenorPreco.Model
{
    public class VendaProduto
    {
        public int ID_VENDA_PRODUTO { get; set; }

        public Produto PRODUTO { get; set; }
        public Venda VENDA { get; set; }

        public long? COD_PRODUTO { get; set; }
        public int ID_VENDA { get; set; }
    }
}

