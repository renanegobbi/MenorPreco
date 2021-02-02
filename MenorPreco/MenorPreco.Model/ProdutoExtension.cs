namespace MenorPreco.Model
{
    public static class ProdutoExtension
    {
        public static ProdutoApi ToApi(this Produto Produto)
        {
            return new ProdutoApi
            {
                cod_gtin = Produto.COD_GTIN,
                cod_produto = Produto.COD_PRODUTO,
                cod_ncm = Produto.COD_NCM,
                cod_unidade = Produto.COD_UNIDADE,
                dsc_produto = Produto.DSC_PRODUTO,
                vlr_unitario = Produto.VLR_UNITARIO,
                id_estabelecimento = Produto.ID_ESTABELECIMENTO,

                url = $"http://maps.google.com/maps?z=18&q={Produto.ESTABELECIMENTO.NUM_LATITUDE.ToString().Substring(0, 3)}.{Produto.ESTABELECIMENTO.NUM_LATITUDE.ToString().Substring(3, Produto.ESTABELECIMENTO.NUM_LATITUDE.ToString().Length - 3)},{Produto.ESTABELECIMENTO.NUM_LONGITUDE.ToString().Substring(0, 3)}.{Produto.ESTABELECIMENTO.NUM_LONGITUDE.ToString().Substring(3, Produto.ESTABELECIMENTO.NUM_LONGITUDE.ToString().Length - 3)}"
            };
        }
    }
}
