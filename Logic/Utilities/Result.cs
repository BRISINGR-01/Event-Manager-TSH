using Shared;
using Shared.Errors;

namespace Logic.Utilities
{
    public class Result<T>
    {
        public Exception? Exception { get; private set; }
        public string ErrorMessage => IsUnSuccessful ? Exception!.Message : throw new DeveloperException("Result is successful and thus there is no error!");
        public T Value => IsSuccessful ? value! : throw new DeveloperException("Result is unsuccessful and the value is unavailable!");
        public bool IsSuccessful { get; private set; } = false;
        public bool IsUnSuccessful => !IsSuccessful;
        private readonly T? value;
        public static Result<T> FailWith(string error) => new(error);
        public Result Fail => new(error: ErrorMessage);

        public Result Plain => IsSuccessful ? new(true) : new(Exception!);

        public Result(Exception exception)
        {
            IsSuccessful = false;
            Exception = exception;
        }
        public Result(string error)
        {
            IsSuccessful = false;
            Exception = new Exception(error);
        }
        public Result(T? value)
        {
            IsSuccessful = value != null;
            this.value = value;
        }

        public static Result<T> From(Func<T?> cb)
        {
            try
            {
                return new(cb() ?? throw new NotFoundException());
            }
            catch (AccessDeniedException ex)
            {
                return new Result<T>(ex);
            }
            catch (ServerException ex)
            {
                Helpers.ReportException(ex);
                return new Result<T>(ex);
            }
            catch (ClientException ex)
            {
                return new Result<T>(ex);
            }
            catch (NotFoundException ex)
            {
                return new Result<T>(ex);
            }
            catch (Exception ex)
            {
                if (ex is DeveloperException) Helpers.ReportException(ex);

                return new Result<T>(error: "An error occurred");
            }
        }
    }

    public class Result
    {
        public Exception Exception { get; private set; } = new Exception();
        public string ErrorMessage => Exception.Message;
        public bool IsSuccessful { get; private set; } = false;
        public bool IsUnSuccessful => !IsSuccessful;
        public static Result Success => new(true);
        public static Result Fail => new(false);
        public static Result FailWith(string error) => new(error: error);
        public Result(bool isSuccessful = true)
        {
            IsSuccessful = isSuccessful;
        }
        public Result(string error)
        {
            IsSuccessful = false;
            Exception = new Exception(error);
        }
        public Result(Exception exception)
        {
            IsSuccessful = false;
            Exception = exception;
        }
        public static Result From(Action cb)
        {
            try
            {
                cb();
                return new(true);
            }
            catch (AccessDeniedException ex)
            {
                return new Result(ex);
            }
            catch (ServerException ex)
            {
                Helpers.ReportException(ex);
                return new Result(ex);
            }
            catch (ClientException ex)
            {
                return new Result(ex);
            }
            catch (NotFoundException ex)
            {
                return new Result(ex);
            }
            catch (Exception ex)
            {
                if (ex is DeveloperException) Helpers.ReportException(ex);

                return new Result(error: "An error occurred");
            }
        }
    }
}
