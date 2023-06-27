namespace InStock.Lib.Exceptions
{
    public abstract class BaseException
        : Exception
    {
        protected static readonly string N = Environment.NewLine;

        public virtual int ErrorCode { get; set; } = 50000;
        
        protected BaseException()
        {

        }

        protected BaseException(string message)
            : base(message)
        {

        }

        protected BaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
