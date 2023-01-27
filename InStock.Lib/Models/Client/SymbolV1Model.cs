namespace InStock.Lib.Models.Client
{
    public class SymbolV1Model
    {
        public SymbolV1Model(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; set; }
    }
}
