namespace InStock.Lib.Exceptions
{
  public class BadRequestException
    : BaseException
  {
    private const string ErrorMessage = "One or more arguments are invalid.";

    public BadRequestException() : base(ErrorMessage) { }

    public BadRequestException(InvalidArgumentException exception)
      : this()
    {
      InvalidArguments.Add(exception);
    }

    /// <inheritdoc />
    public override int ErrorCode { get; set; } = 40000;

    public IList<InvalidArgumentException> InvalidArguments { get; set; } = new List<InvalidArgumentException>();
  }
}
