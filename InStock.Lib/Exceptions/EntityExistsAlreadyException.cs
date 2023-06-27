namespace InStock.Lib.Exceptions
{
    public abstract class EntityExistsAlreadyException
        : BaseException
    {
        public override int ErrorCode { get; set; } = ErrorCodes.Errors.EntityExistsAlready;

        protected EntityExistsAlreadyException(string message)
            : base(message)
        {

        }
    }
}
