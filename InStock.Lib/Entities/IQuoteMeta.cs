namespace InStock.Lib.Entities
{
    public interface IQuoteMeta
    {
        DateTime Date { get; set; }

        decimal Price { get; set; }

        decimal Volume { get; set; }
    }
}
