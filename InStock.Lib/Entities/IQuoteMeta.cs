namespace InStock.Lib.Entities
{
    public interface IQuoteMeta
    {
        DateTime Date { get; }

        double Price { get; }

        long Volume { get; }
    }
}
