namespace InStock.Lib.Entities
{
    public interface IQuoteMeta
    {
        DateTime Date { get; set; }

        double Price { get; set; }

        long Volume { get; set; }
    }
}
