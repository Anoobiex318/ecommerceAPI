using System.Collections.Generic;
using ApplicationException = Ecommerce.Domain.Exceptions.ApplicationException;

namespace Ecommerce.Application.Exception
{
    // Defines a custom exception for validation errors.
    public sealed class ValidationException : ApplicationException
    {
        // Initializes a new instance of the `ValidationException` class.
        // `errorsDictionary` contains the details of the validation errors.
        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Validation Failure", "One or more validation errors occurred")
            => ErrorsDictionary = errorsDictionary;

        // Gets the details of the validation errors.
        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
