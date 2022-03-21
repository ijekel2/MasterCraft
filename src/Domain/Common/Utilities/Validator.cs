using MasterCraft.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Common.Utilities
{
    public class Validator
    {
        Dictionary<string, List<string>> cErrors = new();

        public Dictionary<string, string[]> Errors => cErrors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());

        public bool HasFailure => cErrors.Count() > 0;

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
            if (!cErrors.ContainsKey(propertyName))
            {
                cErrors[propertyName] = new List<string>();
            }

            cErrors[propertyName].Add(message);
        }
    }
}
