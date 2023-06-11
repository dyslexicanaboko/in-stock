namespace InStock.Lib.Exceptions
{
    public abstract class NotFoundBaseException
        : BaseException
    {
        public override int ErrorCode { get; set; } = 40400;

        protected NotFoundBaseException()
        {

        }

        protected NotFoundBaseException(string message)
            : base(message)
        {

        }

        protected NotFoundBaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
