namespace InStock.Lib.Exceptions
{
  public sealed class InvalidArgumentException
    : BaseException
  {
    public InvalidArgumentException(string argument, string message, int errorCode)
      : base(message)
    {
      Argument = argument;

      ErrorCode = errorCode;
    }

    public string Argument { get; set; }
  }
}
