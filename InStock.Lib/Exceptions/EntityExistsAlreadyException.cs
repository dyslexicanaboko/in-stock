namespace InStock.Lib.Exceptions
{
    public abstract class EntityExistsAlreadyException
        : Exception
    {
        protected static readonly string N = Environment.NewLine;

        protected EntityExistsAlreadyException(string message)
            : base(message)
        {

        }
    }
}
