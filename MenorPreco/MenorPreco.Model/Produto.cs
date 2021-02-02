using System.ComponentModel.DataAnnotations.Schema;

namespace MenorPreco.Model
{
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long COD_PRODUTO { get; set; }
        public string DSC_PRODUTO { get; set; }
        public int COD_NCM { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long COD_GTIN { get; set; }
        public decimal VLR_UNITARIO { get; set; }
        public string COD_UNIDADE { get; set; }
        public int ID_ESTABELECIMENTO { get; set; }
        public Estabelecimento ESTABELECIMENTO { get; set; }
        public VendaProduto VENDAPRODUTO { get; set; }
    }


    public class ProdutoApi
    {
        public long cod_produto { get; set; }
        public string dsc_produto { get; set; }
        public int cod_ncm { get; set; }
        public long cod_gtin { get; set; }
        public decimal vlr_unitario { get; set; }
        public string cod_unidade { get; set; }
        public int id_estabelecimento { get; set; }

        public string url { get; set; }
    }
}
