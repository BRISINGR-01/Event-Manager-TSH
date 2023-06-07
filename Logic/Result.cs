using Logic.Interfaces;
using Shared;
using Shared.Errors;
using System.Diagnostics;
using System.Xml.Schema;

namespace Logic
{
    public class Result<T>
    {
        public string? Redirection { get; private set; }
        public string? Error { get; private set; }
        public T Value { get => IsSuccessful ? value! : throw new Exception("Result is unsuccessful and the value is unavailable!"); }
        public bool IsSuccessful { get; private set; } = false;
        public bool ErrorIsDefault { get; private set; } = false;
        private readonly string? area;
        private readonly CRUD action;
        private readonly T? value;
        public static Result<T> FailWith(string error) => new(error: error);
        public Result Fail
        {
            get => new(
                redirection: Redirection,
                error: Error ?? new DatabaseActionException(area, action).Message,
                isDefault: Error == null
            );
        }
        public static Result<T> Success => new();
        public Result Plain => new(error: Error, redirection: Redirection);
        public Result(string? area, CRUD crud, T value)
        {
            this.area = area;
            action = crud;
            this.value = value;
            IsSuccessful = true;
        }

        public Result(string? redirection = null, string? error = null, bool isDefault = false)
        {
            ErrorIsDefault = isDefault;
            Redirection = redirection;
            Error = error;
            try
            {
                if (typeof(T) == typeof(bool)) value = (T)Convert.ChangeType(false, typeof(T));
            } catch { }
        }

        public static Result<T> From(Func<T> cb, CRUD action = CRUD.READ, string? area = null)
        {
            if (area == null)
            {
                Type? type = typeof(T);
                if (type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    Type itemType = type.GetGenericArguments()[0];
                    area = itemType.Name.ToLower() + "s";
                }
                else if (type?.FullName?.StartsWith("Logic.Models") == true  )
                {
                    area = type.Name.ToLower();
                }
                else
                {
                    StackTrace stackTrace = new();
                    StackFrame[] stackFrames = stackTrace.GetFrames();
                    string? name = stackFrames[1].GetMethod()?.DeclaringType?.Name?.Replace("Manager", "").ToLower();
                    if (name != null) area = name;
                }
            }


#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                return new(area, action, cb());
            }
            catch (AccessDeniedException)
            {
                return new Result<T>(
                    redirection: "/Pages/Authentication/AccessDenied", 
                    error: "You are not allowed to execute this action"
                );
            }
            catch (ServerException ex)
            {
                Helpers.ReportException(ex);
                return new Result<T>(redirection: "/Errors/ServerError");
            }
            catch (ClientException ex)
            {
                return new Result<T>(error: ex.Message);
            }
            catch (Exception ex)
            {
                if (ex is DeveloperException) Helpers.ReportException(ex);

                return new Result<T>(
                    error: new DatabaseActionException(area, action).Message, 
                    isDefault: true
                );
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }
    }

    public class Result
    {
        public string? Redirection { get; private set; }
        public string? Error { get; private set; }
        public bool IsSuccessful { get; private set; } = false;
        public bool ErrorIsDefault { get; private set; } = false;
        public static Result Success { get => new(); }
        public static Result FailWith(string error) => new(error: error);
        public Result()
        {
            IsSuccessful = true;
        }
        public Result(string? redirection = null, string? error = null, bool isDefault = false)
        {
            ErrorIsDefault = isDefault;
            Redirection = redirection;
            Error = error;
        }    
    }   
}
