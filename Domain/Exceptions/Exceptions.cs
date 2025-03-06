namespace Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(Dictionary<string, string[]> errors) : base("One or more validation failures have occurred.")
        {
            Errors = errors;
        }
    }

    public class NotFoundError : Exception
    {
        public NotFoundError(string message) : base(message)
        {
        }
    }


    public class UnauthorizedAccess : Exception
    {
        public UnauthorizedAccess(string message) : base(message)
        {
        }
    }

    public class BadRequestError : Exception
    {
        public BadRequestError(string message) : base(message)
        {
        }
    }


    public class ForbiddenError : Exception
    {
        public ForbiddenError(string message) : base(message)
        {
        }
    }


    public class InvalidFileExtension : Exception
    {
        public InvalidFileExtension(string message) : base(message)
        {
        }
    }
}