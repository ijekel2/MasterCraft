using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterCraft.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base(Properties.Resources.OneOrMoreValidationFailuresOccurred)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : this()
        {
            Errors = errors;
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}


