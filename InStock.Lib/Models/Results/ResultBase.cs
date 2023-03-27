namespace InStock.Lib.Models.Results
{
    public class ResultBase
    {
        public bool IsSuccessful { get; set; } = true;

        public string? Message { get; set; }

        public Exception? Exception { get; set; }

        public virtual void Success(string? message = null)
        {
            Message = message;

            IsSuccessful = true;
        }

        public virtual void Failure(Exception ex, string? message = null)
        {
            Message = message;

            Exception = ex;

            IsSuccessful = false;
        }

        public virtual string GetErrorMessage()
        {
            var strException = string.Empty;

            if (Exception != null)
                strException = Exception.Message;

            return $"{strException} {Message}".Trim();
        }
    }
}
