namespace LR_3.ErrorProccesing
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public string? ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
        public static Result<T> Failure(string message, int code) => new() { IsSuccess = false, ErrorMessage = message, ErrorCode = code };
    }
}
