namespace InStock.Lib.Exceptions
{
    public class NotFoundBaseException
        : BaseException
    {
        public override int ErrorCode { get; set; } = 40400;

        protected NotFoundBaseException()
        {

        }

        public NotFoundBaseException(string message)
            : base(message)
        {

        }

        protected NotFoundBaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
