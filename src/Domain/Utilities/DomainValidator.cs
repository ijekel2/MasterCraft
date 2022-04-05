using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Common.Utilities
{
    public class DomainValidator
    {
        Dictionary<string, List<string>> _errors = new();

        public Dictionary<string, string[]> Errors => _errors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());

        public bool HasFailure => _errors.Count() > 0;

        public async Task MustAsync(Func<Task<bool>> condition, string message, string propertyName = "")
        {
            if (!await condition())
            {
                AddFailure(message, propertyName);
            }
        }

        public void Must(Func<bool> condition, string message, string propertyName = "")
        {
            if (!condition())
            {
                AddFailure(message, propertyName);
            }
        }
        private void AddFailure(string message, string propertyName = "")
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            _errors[propertyName].Add(message);
        }
    }
}
